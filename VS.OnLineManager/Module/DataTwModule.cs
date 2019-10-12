using VS.Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.IService;

namespace VS.OnLineManager
{
    public class DataTwModule:IDataTwModule
    {
        private IDataTwService _dataTwService;
        private IMeterageSamplerateService _meterageSamplerateService;
        public DataTwModule(IDataTwService dataTwService, IMeterageSamplerateService meterageSamplerateService)
        {
            _dataTwService = dataTwService;
            _meterageSamplerateService = meterageSamplerateService;
            
        }

        public void GetDataTw(SiteModel site, SocketMiddleware socket)
        {
           Dictionary<string,bool> stopDic = RedisHelper.Get<Dictionary<string, bool>>($"{CallContext.GetData<ClientInfo>("clientInfo").Database}IsStop");
            Console.WriteLine("开始采集波形数据!");
            //存储集合
            List<int> workStatuesList = null;
            List<MeterageSamplerate> list = null;

            int type, frequency, line;
            byte[] cmd = null;
            byte[] recData = new byte[20];
            for (int i = 0; i < site.ChannelStructList.Count; i++)
            {
                //是否停机状态
                string key = $"A{site.ChannelStructList[i].AreaID}M{site.ChannelStructList[i].MachineID}";
                if (stopDic.GetValueOrDefault(key))
                    continue;

                list = _meterageSamplerateService.Query(s =>
                s.AreaId == site.ChannelStructList[i].AreaID &&
                s.McId == site.ChannelStructList[i].MachineID &&
                s.ParId == site.ChannelStructList[i].MonitorID &&
                s.DirId == site.ChannelStructList[i].Position_HVA &&
                s.IsSamplerate == 1);

                workStatuesList = site.ChannelStructList[i].StateStatus.Where(w => w != -1).ToList();
                foreach (MeterageSamplerate item in list)
                {
                    line = item.MsrLine;
                    frequency = item.MsrRateMax;
                    type = item.MsrParameter.ToLower().Contains("env") ? 2 : 0;
                    char typeName = item.MsrName.ToUpper()[0]; //类型的首字母  如：Disp 'D'
                    cmd = OrderHelper.TwDataByChannel(site.ChannelStructList[i].ChannelID, typeName, frequency, line, 0); //获取每个通道取数据命令
                    if (socket.Send(cmd))
                    {
                        if (!socket.Receive(recData))    //接收失败                    
                            continue;

                        int len = BitConverter.ToInt32(recData, 15); //获取要接收的数据长度
                        if (len <= 0) //是否有数据
                            continue;

                        //获取波形数据
                        cmd = OrderHelper.TwDataByChannel(site.ChannelStructList[i].ChannelID, typeName, frequency, line, 1);
                        if (!socket.Send(cmd))
                            continue;

                        byte[] data = new byte[len];
                        if (!socket.Receive(data)) //接收波形数据，最后4个字节是工况
                        {
                            Console.WriteLine(string.Format("{0}站点{1}通道号接收波形异常!", site.Sn, i));
                            continue;
                        }

                        DataTw tw = new DataTw()  //数据转化波形对象
                        {
                            AreaId = item.AreaId,
                            McId = item.McId,
                            ParId = item.ParId,
                            DirId = item.DirId,
                            DataLines = GetLineByType(item.MsrLine, type),
                            DataPoints = (int)(GetLineByType(item.MsrLine, type) * 2.56),
                            DataHz = (int)(GetFrequencyByType(item.MsrRateMax, type) * 2.56),
                            DataIsFFT = 0,
                            DataType = type + 1,
                            DataFormat = 2,
                            Data = new byte[len - 7 * 4],
                            DataWorkStatus = Convert.ToInt32(BitConverter.ToSingle(data, 24)),
                            Time = ConvertTime(data)
                        };

                        //是否是要取的工况
                        if (!workStatuesList.Exists(w => w == tw.DataWorkStatus))
                        {
                            continue;
                        }

                        Array.Copy(data, 7 * 4, tw.Data, 0, len - 7 * 4); //把波形数据copy到 tw.data中

                        if (item.MsrParameter.ToLower().Contains("env")) //包络，直接存
                        {
                            tw.DataIsFFT = 1;
                            tw.Data = SplitMinFFT(tw.Data, item); //截取最小Fmin
                            _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", item.MsrName)); //存入数据库
                        }
                        else if (item.MsrParameter.ToLower().Contains("acc"))
                        {
                            //存储波形
                            _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", item.MsrName));

                            //频谱存储
                            float[] accSpectrum = GetAccSpectrum(ToFloatArray(tw.Data), tw.DataLines);
                            for (int z = 0; z < accSpectrum.Length; z++)
                            {
                                accSpectrum[z] = accSpectrum[z] / 1.414f;//峰值转有效值
                            }
                            tw.Data = ToByteArray(accSpectrum);
                            tw.DataIsFFT = 1;
                            tw.Data = SplitMinFFT(tw.Data, item); //截取最小Fmin
                            _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", item.MsrName));
                        }
                        else if (item.MsrParameter.ToLower().Contains("disp"))
                        {
                            float[] accSpectrum = GetAccSpectrum(ToFloatArray(tw.Data)/*原始波形*/, tw.DataLines);

                            //位移频谱
                            float[] dispSpectrum = GetDispSpectrum(accSpectrum, tw.DataHz, tw.DataPoints);
                            for (int k = 0; k < dispSpectrum.Length; k++)
                            {
                                //位移有效值
                                dispSpectrum[k] = dispSpectrum[k] * 9800 * 1000 / 1.414f;
                            }

                            //位移波形
                            float[] dispTw = new float[tw.DataPoints];
                            FFTHelper.IntegrateFromSpectrum(ToFloatArray(tw.Data)/*原始波形*/, tw.DataHz, tw.DataPoints, item.MsrRateMin, GetFrequencyByType(item.MsrRateMax, type), 2, ref dispTw);
                            for (int k = 0; k < dispTw.Length; k++)
                            {
                                dispTw[k] = dispTw[k] * 9800 * 1000;
                            }

                            //存储波形
                            //tw.data = ToByteArray(dispTw);
                            _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", item.MsrName));

                            //频谱存储
                            tw.DataIsFFT = 1;
                            tw.Data = ToByteArray(dispSpectrum);
                            tw.Data = SplitMinFFT(tw.Data, item); //截取最小Fmin
                            _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", item.MsrName));
                        }
                        else if (item.MsrParameter.ToLower().Contains("vel"))
                        {

                            float[] accSpectrum = GetAccSpectrum(ToFloatArray(tw.Data)/*原始波形*/, tw.DataLines);

                            //速度波形
                            float[] velTw = new float[tw.DataPoints];
                            FFTHelper.IntegrateFromSpectrum(ToFloatArray(tw.Data)/*原始波形*/, tw.DataHz, tw.DataPoints, item.MsrRateMin, GetFrequencyByType(item.MsrRateMax, type), 1, ref velTw);
                            for (int k = 0; k < velTw.Length; k++)
                            {
                                velTw[k] = velTw[k] * 9800;
                            }

                            //速度频谱
                            float[] velSpectrum = GetVelSpectrum(accSpectrum, tw.DataHz, tw.DataPoints);
                            for (int k = 0; k < velSpectrum.Length; k++)
                            {
                                velSpectrum[k] = velSpectrum[k] * 9800 / 1.414f;
                            }

                            //波形存储
                            //tw.data = ToByteArray(velTw);
                            _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", item.MsrName));

                            //频谱存储
                            tw.DataIsFFT = 1;
                            tw.Data = ToByteArray(velSpectrum); //获取频谱
                            tw.Data = SplitMinFFT(tw.Data, item); //截取最小Fmin
                            _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", item.MsrName));
                        }
                    }

                }
            }

            Console.WriteLine("波形数据采集完成！");
        }

        /// <summary>
        /// 截取做小值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        private byte[] SplitMinFFT(byte[] data,MeterageSamplerate ms)
        {
            int type =  ms.MsrParameter.ToLower().Contains("env") ? 2 : 0;
            double temp = (double)GetFrequencyByType(ms.MsrRateMax, type)/GetLineByType(ms.MsrLine,type);
            int count =(int) Math.Ceiling(ms.MsrRateMin / temp);
            for (int i = 0; i <= count*4; i++)
            {
                data[i] = 0;
            }
            return data;
        }

        private DateTime ConvertTime(byte[] data)
        {
            string time = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
                BitConverter.ToSingle(data, 0), //年
                BitConverter.ToSingle(data, 4),  //月
                BitConverter.ToSingle(data, 8),  //日
                BitConverter.ToSingle(data, 12),  //时
                BitConverter.ToSingle(data, 16),  //分
                BitConverter.ToSingle(data, 20));  //秒
            return DateTime.Parse(time);
        }

        /// <summary>
        /// 类型编号 获取对应的分析频率值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private int GetFrequencyByType(int type, int parameter)
        {
            if (parameter == 0)
            {
                switch (type)
                {
                    case 1: return 40000; break;
                    case 2: return 20000; break;
                    case 3: return 10000; break;
                    case 4: return 8000; break;
                    case 5: return 5000; break;
                    case 6: return 4000; break;
                    case 7: return 2000; break;
                    case 8: return 1600; break;
                    case 9: return 1000; break;
                    case 10: return 800; break;
                    case 11: return 500; break;
                    case 12: return 400; break;
                    case 13: return 200; break;
                    case 14: return 100; break;
                    case 15: return 50; break;
                    default: return 0;
                }

            }

            switch (type)
            {
                case 1: return 1000; break;
                case 2: return 500; break;
                default: return 0;
            }
        }

        /// <summary>
        /// 类型编号，获取对应的谱线数值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private int GetLineByType(int line, int type)
        {
            if (type == 0)
            {
                switch (line)
                {
                    case 1: return 400; break;
                    case 2: return 800; break;
                    case 3: return 1600; break;
                    case 4: return 3200; break;
                    case 5: return 6400; break;
                    case 6: return 12800; break;
                    default: return 0;
                }
            }

            switch (line)
            {
                case 1: return 640; break;
                case 2: return 1280; break;
                case 3: return 2560; break;
                default: return 0;
            }
        }

        /// <summary>
        /// byte[] 转化 float[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float[] ToFloatArray(byte[] data)
        {
            int count = (data.Length) / 4;
            float[] tw = new float[count];
            for (int i = 0; i < count; i++)
            {
                tw[i] = BitConverter.ToSingle(data, i * 4);
            }
            return tw;
        }

        /// <summary>
        /// float[] 转化 byte[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private byte[] ToByteArray(float[] data)
        {
            byte[] buffer = new byte[data.Length * 4];
            for (int i = 0; i < data.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(data[i]), 0, buffer, i * 4, 4);
            }
            return buffer;
        }

        /// <summary>
        /// 加速度波形 转化 acc频谱
        /// </summary>
        /// <param name="accTw"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private float[] GetAccSpectrum(float[] accTw, int line)
        {
            float[] spectrum = FFTHelper.TwToFFT(accTw, line);
            return spectrum;
        }

        /// <summary>
        /// 加速频谱 转化 vel频谱
        /// </summary>
        /// <param name="accSpectrum"></param>
        /// <param name="sampleFre"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private float[] GetVelSpectrum(float[] accSpectrum, int sampleFre, int point)
        {
            float[] velSpectrum = new float[point / 2];
            FFTHelper.IntegrateFromSpectrum1(accSpectrum, sampleFre, point, 1, ref velSpectrum);
            return velSpectrum;
        }

        /// <summary>
        /// 加速频谱 转化 disp频谱
        /// </summary>
        /// <param name="accSpectrum"></param>
        /// <param name="sampleFre"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private float[] GetDispSpectrum(float[] accSpectrum, int sampleFre, int point)
        {
            float[] dispSpectrum = new float[point / 2];
            FFTHelper.IntegrateFromSpectrum1(accSpectrum, sampleFre, point, 2, ref dispSpectrum);
            return dispSpectrum;
        }

    }
}
