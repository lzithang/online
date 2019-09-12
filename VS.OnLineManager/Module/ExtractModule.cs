using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VS.Common;
using VS.IService;

namespace VS.OnLineManager
{
    /// <summary>
    /// 特征值提取
    /// </summary>
    public class ExtractModule
    {
        private SiteModel _site;
        private SocketMiddleware _socket;
        private Dictionary<string, List<FeatureItem>> _machineFeatureItem;
        private List<MalfunctionParameter> _parameterList;
        /// <summary>
        /// 服务对象
        /// </summary>
        private ITachoDefindService _tachoDefindService ;
        private IMeterageSamplerateService _meterageSamplerateService;
        private IDataTwService _dataTwService;
        private IDirverRelationService _dirverRelationService;
        private IMachineRevService _machineRevService;
        private IMalfunctionSettingService _malfunctionSettingService;
        private IMalfunctionTypeService _malfunctionTypeService;
        private IMalfunctionParameterService _malfunctionParameterService;

        public ExtractModule(
            ITachoDefindService tachoDefindService,
            IMeterageSamplerateService meterageSamplerateService,
            IDataTwService dataTwService,
            IDirverRelationService dirverRelationService,
            IMachineRevService machineRevService,
            IMalfunctionSettingService malfunctionSettingService,
            IMalfunctionTypeService malfunctionTypeService,
            IMalfunctionParameterService malfunctionParameterService)
        {
            _malfunctionSettingService = malfunctionSettingService;
            _malfunctionTypeService = malfunctionTypeService;
            _machineRevService = machineRevService;
            _dirverRelationService = dirverRelationService;
            _dataTwService = dataTwService;
            _tachoDefindService = tachoDefindService;
            _meterageSamplerateService = meterageSamplerateService;

            if (!RedisHelper.Exists("MalParameter"))
            {
                _parameterList = _malfunctionParameterService.QueryList();
                RedisHelper.Set("MalParameter", _parameterList);
            }
            else
            {
                _parameterList = RedisHelper.Get<List<MalfunctionParameter>>("MalParameter");
            }
           
        }

        /// <summary>
        /// 初始化，处理波形频谱需要的参数信息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="socket"></param>
        public void InitConfig(SiteModel site, SocketMiddleware socket)
        {
            _site = site;
            _socket = socket;
            List<MeterageSamplerate> meterageSamplerateList = _meterageSamplerateService.Query(ms => ms.AreaId == _site.AearId);
            List<TachoDefind> tachoDefindList = _tachoDefindService.Query(td => td.AreaId == _site.AearId);
            foreach (TachoDefind tachoDefind in tachoDefindList)
            {
                MeterageSamplerate ms = meterageSamplerateList.FirstOrDefault(m => m.MsrId == tachoDefind.MsrId && m.IsSamplerate == 1);
                if (ms == null)
                    continue;
                //获取频谱数据
                DataTw tw = GetTwFFTData(ms);
                if (tw == null)
                    continue;
                float[] data = ToFloatArray(tw.Data);
                float rev =  ExtractRPM(data,tw.DataHz/((float)tw.DataLines), tachoDefind);
                GetMalfunctionSettingList(ms, (int)rev);
            }

            foreach (MeterageSamplerate ms in meterageSamplerateList)
            {
                if (tachoDefindList.FirstOrDefault(t => t.MsrId == ms.MsrId) != null)
                    continue;
                DataTw tw = GetTwFFTData(ms);
                float[] data = ToFloatArray(tw.Data);
                GetMalfunctionSettingList(ms);
            }

        }

        private List<MalfunctionSetting> GetMalfunctionSettingList(MeterageSamplerate ms,int rev = -1)
        {
            string key = $"A{ms.AreaId}M{ms.McId}";
            List<FeatureItem> featureList = null;
            if (!_machineFeatureItem.ContainsKey(key))
            {
                DirverHelper dirverHelper = new DirverHelper();
                dirverHelper.InitDirverData(ms.AreaId, ms.McId, _dirverRelationService, _machineRevService);
                dirverHelper.CreateFeatureItemList(0, rev);
                featureList = dirverHelper._featureItemList;
                _machineFeatureItem.Add(key, dirverHelper._featureItemList);
            }
            else
            {
                featureList = _machineFeatureItem[key];
            }
            
            List<MalfunctionSetting> settingList = _malfunctionSettingService.GetMalfunctionSettingListByMsrId(ms.MsrId);
            foreach (MalfunctionSetting item in settingList)
            {
                FeatureItem feature = featureList.FirstOrDefault(f => f.FMtId == item.UnitType && f.FKey == item.UnitId);
                List<MalfunctionParameter> parameterList = _parameterList.FindAll(p => p.MtId == item.MtId);
                foreach (var parameter in parameterList)
                {
                    item.CommonFormula = CalcParameter(item.CommonFormula,parameter.MpName, feature);
                    item.RemoveFrequency = CalcParameter(item.RemoveFrequency, parameter.MpName, feature);
                    item.SidebandFrequency = CalcParameter(item.SidebandFrequency, parameter.MpName, feature);
                    item.CenterFrequency = CalcParameter(item.CenterFrequency, parameter.MpName, feature);
                }
            }
            return settingList;
        }

        /// <summary>
        /// 参数替换
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private string CalcParameter(string expression, string paras,FeatureItem feature)
        {
            string strTemp = expression;
            if (!string.IsNullOrEmpty(expression))
            {
                if (expression.ToLower().Contains(paras))
                {
                    strTemp = CalcParameter(expression.ToLower().Replace(paras, feature[paras].ToString("#0.00")));
                }
            }
            return strTemp;
        }

        /// <summary>
        /// 计算参数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string CalcParameter(string expression)
        {
            //是否包含英文字符，包含继续替换
            string pattern = @"[A-Za-z]";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(expression))
            {
                return expression;
            }

            string[] itemArray = null;
            char solit = ',';
            if (expression.IndexOf(',') > -1)
            {
                itemArray = expression.Split(new char[] { solit });
            }
            else if (expression.IndexOf('_') > -1)
            {
                solit = '_';
                itemArray = expression.Split(new char[] { solit });
            }
            else
            {
                itemArray = new string[] { expression };
            }
            string strTemp = string.Empty;
            if (itemArray != null && itemArray.Length > 0)
            {
                foreach (string item in itemArray)
                {
                   strTemp += CalcHelper.CalcExpression(item) + solit.ToString();
                }
                strTemp = strTemp.Substring(0, strTemp.Length - 1);
            }
            return strTemp;
        }

        /// <summary>
        /// 从下位机获取波形或频谱数据，波形数据直接存储，频谱数据返回供提取转速
        /// </summary>
        /// <param name="meterageSamplerate">获取波形或频谱参数</param>
        /// <returns></returns>
        private DataTw GetTwFFTData(MeterageSamplerate ms)
        {
            byte[] recData = new byte[20];
            int line = ms.MsrLine;
            int frequency = ms.MsrRateMax;
            int type = ms.MsrParameter.ToLower().Contains("env") ? 2 : 0;
            char typeName = ms.MsrName.ToUpper()[0]; //类型的首字母  如：Disp 'D'
            ChannelStruct channelStruct = _site.ChannelStructList.FirstOrDefault(cs => cs.AreaID == ms.AreaId && cs.MachineID == ms.McId && cs.MonitorID == ms.ParId && cs.Position_HVA == ms.DirId);//获取通道号
            List<int> workStatuesList = channelStruct.StateStatus.Where(w => w != -1).ToList();

            byte[] cmd = OrderHelper.TwDataByChannel(channelStruct.ChannelID, typeName, frequency, line, 0); //获取每个通道取数据命令
            if (_socket.Send(cmd))
            {
                if (!_socket.Receive(recData))    //接收失败                    
                    return null;

                int len = BitConverter.ToInt32(recData, 15); //获取要接收的数据长度
                if (len <= 0) //是否有数据
                    return null;

                //获取波形数据
                cmd = OrderHelper.TwDataByChannel(channelStruct.ChannelID, typeName, frequency, line, 1);
                if (!_socket.Send(cmd))
                    return null;

                byte[] data = new byte[len];
                if (!_socket.Receive(data)) //接收波形数据，最后4个字节是工况
                {
                    Console.WriteLine(string.Format("{0}站点{1}通道号接收波形异常!", _site.Sn, channelStruct.ChannelID));
                    return null;
                }

                DataTw tw = new DataTw()  //数据转化波形对象
                {
                    AreaId = ms.AreaId,
                    McId = ms.McId,
                    ParId = ms.ParId,
                    DirId = ms.DirId,
                    DataLines = GetLineByType(ms.MsrLine, type),
                    DataPoints = (int)(GetLineByType(ms.MsrLine, type) * 2.56),
                    DataHz = (int)(GetFrequencyByType(ms.MsrRateMax, type) * 2.56),
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
                    return null;
                }

                Array.Copy(data, 7 * 4, tw.Data, 0, len - 7 * 4); //把波形数据copy到 tw.data中

                if (ms.MsrParameter.ToLower().Contains("env")) //包络，直接存
                {
                    tw.DataIsFFT = 1;
                    tw.Data = SplitMinFFT(tw.Data, ms); //截取最小Fmin
                    _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", ms.MsrName)); //存入数据库
                }
                else if (ms.MsrParameter.ToLower().Contains("acc"))
                {
                    //存储波形
                    _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", ms.MsrName));

                    //频谱存储
                    float[] accSpectrum = GetAccSpectrum(ToFloatArray(tw.Data), tw.DataLines);
                    for (int z = 0; z < accSpectrum.Length; z++)
                    {
                        accSpectrum[z] = accSpectrum[z] / 1.414f;//峰值转有效值
                    }
                    tw.Data = ToByteArray(accSpectrum);
                    tw.DataIsFFT = 1;
                    tw.Data = SplitMinFFT(tw.Data, ms); //截取最小Fmin
                    _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", ms.MsrName));
                }
                else if (ms.MsrParameter.ToLower().Contains("disp"))
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
                    FFTHelper.IntegrateFromSpectrum(ToFloatArray(tw.Data)/*原始波形*/, tw.DataHz, tw.DataPoints, ms.MsrRateMin, GetFrequencyByType(ms.MsrRateMax, type), 2, ref dispTw);
                    for (int k = 0; k < dispTw.Length; k++)
                    {
                        dispTw[k] = dispTw[k] * 9800 * 1000;
                    }

                    //存储波形
                    //tw.data = ToByteArray(dispTw);
                    _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", ms.MsrName));

                    //频谱存储
                    tw.DataIsFFT = 1;
                    tw.Data = ToByteArray(dispSpectrum);
                    tw.Data = SplitMinFFT(tw.Data, ms); //截取最小Fmin
                    _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", ms.MsrName));
                }
                else if (ms.MsrParameter.ToLower().Contains("vel"))
                {

                    float[] accSpectrum = GetAccSpectrum(ToFloatArray(tw.Data)/*原始波形*/, tw.DataLines);

                    //速度波形
                    float[] velTw = new float[tw.DataPoints];
                    FFTHelper.IntegrateFromSpectrum(ToFloatArray(tw.Data)/*原始波形*/, tw.DataHz, tw.DataPoints, ms.MsrRateMin, GetFrequencyByType(ms.MsrRateMax, type), 1, ref velTw);
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
                    _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", ms.MsrName));

                    //频谱存储
                    tw.DataIsFFT = 1;
                    tw.Data = ToByteArray(velSpectrum); //获取频谱
                    tw.Data = SplitMinFFT(tw.Data, ms); //截取最小Fmin
                    _dataTwService.InsertDataTw(tw, string.Format("tb_data_tw_{0}", ms.MsrName));
                }
                return tw;
            }

            return null;
        }

        #region 波形频谱数据处理方法
        /// <summary>
        /// 截取做小值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        private byte[] SplitMinFFT(byte[] data, MeterageSamplerate ms)
        {
            int type = ms.MsrParameter.ToLower().Contains("env") ? 2 : 0;
            double temp = (double)GetFrequencyByType(ms.MsrRateMax, type) / GetLineByType(ms.MsrLine, type);
            int count = (int)Math.Ceiling(ms.MsrRateMin / temp);
            for (int i = 0; i <= count * 4; i++)
            {
                data[i] = 0;
            }
            return data;
        }

        /// <summary>
        /// 波形时间转化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
        #endregion

        /// <summary>
        /// 提取转速
        /// </summary>
        /// <param name="data">频谱数据</param>
        /// <param name="ratio">分辨率</param>
        /// <param name="tachoDefind">转速提取条件</param>
        /// <returns></returns>
        public float ExtractRPM(float[] data,float ratio,TachoDefind tachoDefind)
        {
            float RpmF = 0.0f;
            float HzF = 0.0f;
 
                float nLeft = tachoDefind.TdHzMin;
                float nRight = tachoDefind.TdHzMax;
                int startIndex = (int)(tachoDefind.TdHzMin / ratio);
                Dictionary<int, float> dataAmp = new Dictionary<int, float>();
                for (int i = startIndex; i < data.Length; i++)
                {
                    if (i * ratio > nRight)
                        break;
                    dataAmp.Add(i, data[i]);
                }
                if (dataAmp.Count > 0)
                {
                    foreach (KeyValuePair<int, float> keyValue in dataAmp)
                    {
                        if (keyValue.Value > RpmF && keyValue.Value > tachoDefind.TdAmpMin)
                        {
                            RpmF = keyValue.Value;
                            HzF = keyValue.Key * ratio;
                        }
                    }
                }
            
            return HzF * 60;
        }


    }
}
