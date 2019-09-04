using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VS.IService;

namespace VS.OnLineManager
{
    public class ConfigModule:IConfigModule
    {
        public SiteModel _site { get; set; }
        public SocketMiddleware _sokect { get; set; }
        private IMeterageSamplerateService _meterageSamplerateService;
        public ConfigModule(IMeterageSamplerateService meterageSamplerateService)
        {
            _meterageSamplerateService = meterageSamplerateService;
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public void UpdateTime()
        {
            byte[] cmd = OrderHelper.UpdateTime();
            _sokect.Send(cmd);
            byte[] recData = new byte[13];
            if (_sokect.Receive(recData))
            {
                if (recData[1] == 'P' && recData[2] == 'S' && recData[3] == 'O' && recData[4] == 'K')
                {
                    Console.WriteLine("更新时间成功！");
                    return;
                }

            }
            Console.WriteLine(string.Format("{0}站点时间更新失败！", _site.Sn));
        }

        public void UpdateSiteConfig()
        {
            //更新行程
            UpdateSiteStatus();
            //更新通道参数
            UpdateChannelInfo();
            //更新测量参数 
            UpdateMeterageSample();
        }

        /// <summary>
        /// 更新通道
        /// </summary>
        private void UpdateChannelInfo()
        {
            byte[] recData = new byte[13];
            Console.WriteLine("更新通道绑定信息！");
            //更新通道配置信息
            foreach (ChannelStruct item in _site.ChannelStructList)
            {
                byte[] cmd = OrderHelper.UpdateChannelInfo(item);
                if (_sokect.Send(cmd))
                {
                    if (_sokect.Receive(recData))
                    {
                        if (recData[1] == 'P' && recData[2] == 'S' && recData[3] == 'O' && recData[4] == 'K')
                        {
                            continue;
                        }
                        //LogHelper.Info(MethodBase.GetCurrentMethod(), string.Format("{0}站点{1}通道号绑定信息更新失败！", _site.Sn, item.ChannelID));
                    }
                }
            }
            Console.WriteLine("更新通道绑定信息完成！");
        }

        /// <summary>
        /// 更新调理版设置组
        /// </summary>
        private void UpdateMeterageSample()
        {
            //设置每块调理版的参数组
            byte[] recData = new byte[13];
            Console.WriteLine("更新调理版参数组！");
            for (int i = 1; i <= 4; i++)
            {
                byte[] cmd = OrderHelper.UpdateMeterageSample(_site.MeterageLibList.Where(item => item.GorupId == i).ToList(), i);
                if (cmd != null)
                {
                    _sokect.Send(cmd);
                    if (!_sokect.Receive(recData)) ;
                    //LogHelper.Info(MethodBase.GetCurrentMethod(), string.Format("{0}站点更新调理{1}号参数组失败！", _site.Sn, i));
                }
            }

            //设置对应每块调理版的参数组是否采集
            for (int i = 1; i <= 4; i++)
            {
                List<MeterageLibModel> meterageLibList = _site.MeterageLibList.Where(item => item.GorupId == i).ToList();
                if (meterageLibList.Count == 0)
                    continue;
                for (int j = 1; j <= 8; j++)
                {
                    int channelId = j + (i - 1) * 8;
                    IEnumerable<ChannelStruct> channelStructList = _site.ChannelStructList.Where(c => c.ChannelID == channelId);
                    if (channelStructList.Count() == 0)
                        continue;

                    ChannelStruct channelStruct = channelStructList.FirstOrDefault();
                    List<MeterageSamplerate> meterageSampleList = _meterageSamplerateService.Query(s =>
                    s.AreaId == channelStruct.AreaID &&
                    s.McId == channelStruct.MachineID &&
                    s.ParId == channelStruct.MonitorID &&
                    s.DirId == channelStruct.Position_HVA &&
                    s.IsSamplerate == 1);
                    byte[] cmd = OrderHelper.UpdateMeterageChannelBind(meterageLibList, meterageSampleList, channelId);
                    if (cmd != null)
                    {
                        if (_sokect.Send(cmd))
                        {
                            if (_sokect.Receive(recData))
                            {
                                continue;
                            }
                            //LogHelper.Info(MethodBase.GetCurrentMethod(), string.Format("{0}站点{1}通道号参数组是否采集配置失败！", _site.Sn, channelId));
                        }
                    }
                }
            }
            Console.WriteLine("更新调理版参数组完成！");
        }

        /// <summary>
        /// 更新工况
        /// </summary>
        private void UpdateSiteStatus()
        {
            Console.WriteLine("更新站点行程！");
            byte[] sendData = OrderHelper.UpdateStatus(_site.WorkStatusList);
            byte[] revData = new byte[13];
            if (_sokect.Send(sendData))
            {
                if (_sokect.Receive(revData))
                {
                    Console.WriteLine("站点行程更新成功！");
                    return;
                }
            }
            Console.WriteLine("站点行程更新失败！");
        }

        /// <summary>
        /// 固件更新
        /// </summary>
        public void UpdateSolid()
        {
            Console.WriteLine("开始固件更新！");
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileSolid\\clientVib");
                FileStream file = File.Open(filePath, FileMode.Open);
                byte[] data = new byte[file.Length];
                int fileLength = (int)file.Length;
                int readLength = 0;
                while (true)
                {
                    readLength += file.Read(data, 0, (int)file.Length);
                    if (readLength >= fileLength)
                        break;
                }

                file.Close();
                // file.Flush();
                if (readLength == fileLength)
                {
                    byte[] revData = new byte[13];
                    byte[] cmd = OrderHelper.SolidHandshake(readLength);
                    if (_sokect.Send(cmd))  //发送固件长度
                    {
                        if (_sokect.Receive(revData))
                        {
                            if (revData[7] == (byte)'O' && revData[8] == (byte)'K')
                            {
                                cmd = OrderHelper.UpdateSolid(data);
                                if (_sokect.Send(cmd)) //发送固件
                                {
                                    if (_sokect.Receive(revData))
                                    {
                                        if (revData[7] == (byte)'O' && revData[8] == (byte)'K')
                                        {
                                            Console.WriteLine("固件更新完毕！");
                                            return;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                Console.WriteLine("固件更新失败！");
            }
            catch (Exception ex)
            {
                Program.LoggerHelper.Error(typeof(ConfigModule), ex.Message, ex);
                Console.WriteLine("更新固件异常！{0}", ex);
            }
        }
    }
}
