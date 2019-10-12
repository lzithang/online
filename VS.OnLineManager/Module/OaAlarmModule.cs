using Autofac;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Common;
using VS.IService;

namespace VS.OnLineManager
{
    public class OaAlarmModule : IOaAlarmModule
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private IAlarmService _alarmService;
        private IMeterageService _meterageService;
        /// <summary>
        /// 对应工厂的总值队列数据
        /// </summary>
        private DataOaQueue _dataOaQueue;
        /// <summary>
        /// 报警次数
        /// </summary>
        private int _alarmCount;
        /// <summary>
        /// 下位机模块信息
        /// </summary>
        private ClientInfo _clientInfo;

        /// <summary>
        /// 初始化 需要处理数据的信息
        /// </summary>
        /// <param name="alarmService"></param>
        /// <param name="meterageService"></param>
        public OaAlarmModule(IAlarmService alarmService, IMeterageService meterageService)
        {
            _clientInfo = CallContext.GetData<ClientInfo>("clientInfo");
            _dataOaQueue = RedisHelper.Get<DataOaQueue>(_clientInfo.Database);
            if (_dataOaQueue == null)
            {
                _dataOaQueue = new DataOaQueue();
            }
            _alarmCount = Convert.ToInt32(AppsettingsHelper.GetConfigString("AlarmCount"));
            _alarmService = alarmService;
            _meterageService = meterageService;
        }

        /// <summary>
        /// 验证报警
        /// </summary>
        /// <param name="dataOaList"></param>
        public void ValidateAlarm(List<DataOa> dataOaList, Meterage meterage)
        {
            //获取内存中 方向下的总值数据信息
            string key = $"{meterage.AreaId}{meterage.McId}{meterage.ParId}{meterage.DirId}";
            Queue<DataOa> oaDataQueue = _dataOaQueue.DataQueue.GetValueOrDefault(key);
            int dilutedCount = _dataOaQueue.DilutedCount.GetValueOrDefault(key);
            if (oaDataQueue == null)
            {
                oaDataQueue = new Queue<DataOa>();
            }

            //总值进队
            foreach (DataOa item in dataOaList)
            {
                oaDataQueue.Enqueue(item);
                Console.WriteLine($"Bg:{item.OaBg},BV:{item.OaBv},Env:{item.OaEnv},Acc:{item.OaAcc},kurt:{item.OaKurt},Vel:{item.OaVel},CF:{item.OaCF},time:{item.OaTime.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

            //稀释数据
            if((oaDataQueue.Count - dilutedCount *12) >= 12)
            {
                List<DataOa> dilutedOa =  oaDataQueue.Skip(dilutedCount * 12).Take(12).ToList();
                AvgDataOaSave(dilutedOa, 1);
                ++dilutedCount;
                if (dilutedCount > 5)
                {
                    AvgDataOaSave(oaDataQueue.ToList(), 2);
                    while (oaDataQueue.Count > _alarmCount)
                    {
                        DataOa item = oaDataQueue.Dequeue();
                    }
                    dilutedCount = 0;
                }
            }

            _dataOaQueue.DataQueue[key] = oaDataQueue;
            _dataOaQueue.DilutedCount[key] = dilutedCount;
            List<Alarm> alarmList = _alarmService.Query(a => a.AlarmId == meterage.MeterAlarm1 || a.AlarmId == meterage.MeterAlarm2);

            Console.WriteLine($"一级报警线：Bg:{alarmList[0].AlarmBg},BV:{alarmList[0].AlarmBv},Env:{alarmList[0].AlarmEnv},Acc:{alarmList[0].AlarmAcc},kurt:{alarmList[0].AlarmKurt},Vel:{alarmList[0].AlarmVel},CF:{alarmList[0].AlarmCF}");
            Console.WriteLine($"二级报警线：Bg:{alarmList[1].AlarmBg},BV:{alarmList[1].AlarmBv},Env:{alarmList[1].AlarmEnv},Acc:{alarmList[1].AlarmAcc},kurt:{alarmList[1].AlarmKurt},Vel:{alarmList[1].AlarmVel},CF:{alarmList[1].AlarmCF}");

            //取最新几笔数据 计算报警次数
            int len = oaDataQueue.Count;
            List<DataOa> oaList = null;
            if(len > _alarmCount)
                oaList = oaDataQueue.Skip(len - _alarmCount).Take(_alarmCount).ToList();
            else
                oaList = oaDataQueue.ToList();
            AlarmTimes alarmTimes = GetAlarmTimes(oaList, alarmList, meterage);

            //报警状态判断
            int alarmGrad = 1;
            if (alarmTimes.GetAlarm1Times() == _alarmCount)
            {
                alarmGrad = 2;
                if (alarmTimes.GetAlarm2Times() == _alarmCount)
                {
                    alarmGrad = 3;
                }
                CreateEmail(alarmTimes, dataOaList[0], alarmList[0], alarmList[1], alarmGrad);
            }

            //修改报警状态
            if (alarmGrad != meterage.MeterStateOa)
            {
                meterage.MeterStateOa = alarmGrad;
                _meterageService.UpdateEntity(meterage);
            }
        }

        /// <summary>
        /// 存储缓存数据
        /// </summary>
        public void SaveCache()
        {
            RedisHelper.Set(_clientInfo.Database, _dataOaQueue);
        }

        /// <summary>
        /// 构建邮件
        /// </summary>
        /// <param name="alarmTimes"></param>
        private void CreateEmail(AlarmTimes alarmTimes, DataOa dataOa, Alarm alarm1, Alarm alarm2, int alarmGrad)
        {
            string oaType = string.Empty;
            if (alarmGrad == 2)
            {
                if (alarmTimes.Alarm1.FirstOrDefault(a => a.Key == "Temp").Value == _alarmCount)
                    oaType = "Temp";

                if (alarmTimes.Alarm1.FirstOrDefault(a => a.Key == "Vel").Value == _alarmCount)
                    oaType = "Vel";
                if (string.IsNullOrEmpty(oaType))
                    return;
                //oaType = alarmTimes.Alarm1.FirstOrDefault(t => t.Value >= _alarmCount).Key;
            }
            else
            {
                if (alarmTimes.Alarm1.FirstOrDefault(a => a.Key == "Temp").Value == _alarmCount)
                    oaType = "Temp";

                if (alarmTimes.Alarm1.FirstOrDefault(a => a.Key == "Vel").Value == _alarmCount)
                    oaType = "Vel";
                if (string.IsNullOrEmpty(oaType))
                    return;
                //oaType = alarmTimes.Alarm2.FirstOrDefault(t => t.Value >= _alarmCount).Key;
            }

            //是否发送过邮件
            string key = $"{_clientInfo.Database}{dataOa.AreaId}{dataOa.McId}{dataOa.ParId}{dataOa.DirId}";
            if (RedisHelper.Get<bool>(key))
            {
                return;
            }
            RedisHelper.Set(key, true, 60*60*24);

            //报警的总值类型
            string unit = "";
            double value = 0;
            float alarmLine1 = 0;
            float alarmLine2 = 0;
            PathName pathName = Program.container.Resolve<IAreaService>().GetPathName(dataOa.AreaId, dataOa.McId, dataOa.ParId);
            string dirName = dataOa.DirId == 1 ?"H":dataOa.DirId == 2 ?"V":"A-T";
            string ftName = _clientInfo.FtName;
            string color = alarmGrad == 2 ? "#ff9900" : "#ed4014";
            string grad = alarmGrad == 2 ? "警告" : "危险";
            string dataBase = _clientInfo.Database;
            _clientInfo.Database = "bpdm_user";
            CallContext.SetData("clientInfo", _clientInfo);
            List<UserInfo> users = Program.container.Resolve<IUserInfoService>().Query(u => u.CpId == _clientInfo.CpId);
            _clientInfo.Database = dataBase;
            CallContext.SetData("clienInfo", _clientInfo);

            switch (oaType)
            {
                case "Acc":
                    value = dataOa.OaAcc;
                    unit = "g RMS";
                    alarmLine1 = alarm1.AlarmAcc;
                    alarmLine2 = alarm2.AlarmAcc; break;
                case "Vel":
                    unit = "mm/s RMS";
                    value = dataOa.OaVel;
                    alarmLine1 = alarm1.AlarmVel;
                    alarmLine2 = alarm2.AlarmVel; break;
                case "Disp":
                    unit = "μm/s RMS";
                    value = dataOa.OaDisp;
                    alarmLine1 = alarm1.AlarmDisp;
                    alarmLine2 = alarm2.AlarmDisp; break;
                case "Env":
                    unit = "g RMS";
                    value = dataOa.OaEnv;
                    alarmLine1 = alarm1.AlarmEnv;
                    alarmLine2 = alarm2.AlarmEnv; break;
                case "CF":
                    value = dataOa.OaCF;
                    alarmLine1 = alarm1.AlarmCF;
                    alarmLine2 = alarm2.AlarmCF; break;
                case "Kurt":
                    value = dataOa.OaKurt;
                    alarmLine1 = alarm1.AlarmKurt;
                    alarmLine2 = alarm2.AlarmKurt; break;
                case "Temp":
                    unit = "℃";
                    value = dataOa.OaTemp;
                    alarmLine1 = alarm1.AlarmT;
                    alarmLine2 = alarm2.AlarmT; break;
                case "Bg":
                    unit = "g RMS";
                    value = dataOa.OaBg;
                    alarmLine1 = alarm1.AlarmBg;
                    alarmLine2 = alarm2.AlarmBg; break;
                case "Bv":
                    unit = "mm/s RMS";
                    value = dataOa.OaBv;
                    alarmLine1 = alarm1.AlarmBv;
                    alarmLine2 = alarm2.AlarmBv; break;
                default:
                    break;
            }
            foreach (UserInfo user in users)
            {
                if (string.IsNullOrEmpty(user.UserEmail))
                    continue;
               
                EmailModel model = new EmailModel()
                {
                    SenderAddress = "vs-view@bpdm.com.cn",
                    SenderName = "VS-View云在线监测系统",
                    SenderPassword = "Pdm@1108",
                    Title = "VS-View 状态监测云报警通知",
                    ReceiverAddress = user.UserEmail
                };
                StringBuilder sb = new StringBuilder();
                sb.Append("<table  border='0' bgcolor='white' cellpadding='0' cellspacing='1'>");
                sb.Append($"<tr style='background-color:{color};'><td style='padding:5px 10px;color:white' colspan=3>报警等级：{grad}</td></tr>");
                sb.Append($"<tr style='background-color:#D0D8E8;'><td style='padding:5px 10px;'>日期/时间</td><td style='padding:5px; 10px'>{DateTime.Now.ToString("yyyy-MM-dd")}</td><td style='padding:5px; 10px'>{DateTime.Now.ToString("HH:mm:ss")}</td></tr>");
                sb.Append($"<tr style='background-color:#E9EDF4;'><td style='padding:5px 10px;'>工厂/区域/机器/测点/方向</td><td colspan=2 style='padding:5px; 10px'>{ftName}/{pathName.AreaName}/{pathName.McName}/{pathName.ParName}/{dirName}</td></tr>");
                sb.Append($"<tr style='background-color:#D0D8E8;'><td style='padding:5px 10px;'>测量值({oaType})</td><td colspan=2 style='padding:5px; 10px'><span style='color:{color}'>{Math.Round(value, 2)}</span> {unit}</td></tr>");
                sb.Append($"<tr style='background-color:#E9EDF4;'><td style='padding:5px 10px;'>阈值</td><td style='padding:5px; 10px'>警告：{alarmLine1} {unit}</td><td style='padding:5px; 10px'>危险：{alarmLine2} {unit}</td></tr></table>");
                sb.Append("<br/><br/><p>警告 – 机器存在问题，需查明原因或限制运行</p>");
                sb.Append("<p>危险 – 机器存在严重问题，需查明原因并安排方便的时间停机检修</p>");
                sb.Append("<p>详情分析请登录<a href='http://www.bpdm.cloud'>http://www.bpdm.cloud</a></p>");

                model.Content = sb.ToString();
                Program.EmailModelQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 获取总值每个参数的报警次数
        /// </summary>
        /// <param name="oaList"></param>
        /// <param name="alarmList"></param>
        /// <param name="meterage"></param>
        /// <returns></returns>
        private AlarmTimes GetAlarmTimes(List<DataOa> oaList, List<Alarm> alarmList, Meterage meterage)
        {
            AlarmTimes alarmTimes = new AlarmTimes();
            Alarm alarm1 = alarmList[0];
            Alarm alarm2 = alarmList[1];
            foreach (DataOa oAData in oaList)
            {
                if (alarm1 != null && alarm2 != null)
                {
                    float p_float_alarmTemp = (float)oAData.OaAcc;
                    //-----------------------------------acc-------------------------------------------
                    if (meterage.MeterAcc == 1 && alarm1.AlarmAcc != 0 && alarm2.AlarmAcc != 0)
                    {
                        //注意
                        if (alarm1.AlarmAcc < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Acc");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmAcc)
                        {
                            alarmTimes.AddAlarm2("Acc");
                        }
                    }
                    p_float_alarmTemp = (float)oAData.OaDisp;
                    //-----------------------------------disp-------------------------------------------
                    if (meterage.MeterDisp == 1 && alarm1.AlarmDisp != 0 && alarm2.AlarmDisp != 0)
                    {

                        if (alarm1.AlarmDisp < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Disp");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmDisp)
                        {
                            alarmTimes.AddAlarm2("Disp");
                        }
                    }
                    p_float_alarmTemp = (float)oAData.OaVel;
                    //------------------------------------vel----------------------------------------------
                    if (meterage.MeterVel == 1 && alarm1.AlarmVel != 0 && alarm2.AlarmVel != 0)
                    {
                        //注意
                        if (alarm1.AlarmVel < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Vel");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmVel)
                        {
                            alarmTimes.AddAlarm2("Vel");
                        }
                    }
                    //------------------------------------bv----------------------------------------------
                    p_float_alarmTemp = (float)oAData.OaBv;
                    if (meterage.MeterBv == 1 && alarm1.AlarmBv != 0 && alarm2.AlarmBv != 0)
                    {
                        //注意
                        if (alarm1.AlarmBv < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Bv");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmBv)
                        {
                            alarmTimes.AddAlarm2("Bv");
                        }
                    }
                    //------------------------------------bg----------------------------------------------
                    if (meterage.MeterBg == 1 && alarm1.AlarmBg != 0 && alarm2.AlarmBg != 0)
                    {
                        //注意
                        if (alarm1.AlarmBg < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Bg");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmBg)
                        {
                            alarmTimes.AddAlarm2("Bg");
                        }
                    }
                    //------------------------------------env----------------------------------------------
                    p_float_alarmTemp = (float)oAData.OaEnv;
                    if (meterage.MeterEnv == 1 && alarm1.AlarmEnv != 0 && alarm2.AlarmEnv != 0)
                    {
                        //注意
                        if (alarm1.AlarmEnv < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Env");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmEnv)
                        {
                            alarmTimes.AddAlarm2("Env");
                        }
                    }
                    //------------------------------------temp----------------------------------------------
                    p_float_alarmTemp = (float)oAData.OaTemp;
                    if (meterage.MeterT == 1 && alarm1.AlarmT != 0 && alarm2.AlarmT != 0)
                    {
                        //注意
                        if (alarm1.AlarmT < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Temp");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmT)
                        {
                            alarmTimes.AddAlarm2("Temp");
                        }


                    }
                    //------------------------------------cf----------------------------------------------
                    p_float_alarmTemp = (float)oAData.OaCF;
                    if (meterage.MeterCF == 1 && alarm1.AlarmCF != 0 && alarm2.AlarmCF != 0)
                    {
                        //注意
                        if (alarm1.AlarmCF < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("CF");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmCF)
                        {
                            alarmTimes.AddAlarm2("CF");
                        }
                    }
                    //------------------------------------kurt----------------------------------------------
                    p_float_alarmTemp = (float)oAData.OaKurt;
                    if (meterage.MeterKurt == 1 && alarm1.AlarmKurt != 0 && alarm2.AlarmKurt != 0)
                    {
                        //注意
                        if (alarm1.AlarmKurt < p_float_alarmTemp)
                        {
                            alarmTimes.AddAlarm1("Kurt");
                        }
                        //报警
                        if (p_float_alarmTemp > alarm2.AlarmKurt)
                        {
                            alarmTimes.AddAlarm2("Kurt");
                        }


                    }

                }
            }
            return alarmTimes;
        }

        /// <summary>
        /// 求平均，保存总值
        /// </summary>
        /// <param name="oaList"></param>
        /// <param name="type">1总值月表，2总值年表</param>
        private void AvgDataOaSave(List<DataOa> oaList,int type)
        {
            int len = oaList.Count;
            if (type == 1)
            {
                DataOaMonth oa = new DataOaMonth()
                {
                    AreaId = oaList[0].AreaId,
                    McId = oaList[0].McId,
                    ParId = oaList[0].ParId,
                    DirId = oaList[0].DirId,
                    OaWorkStatus = oaList[0].OaWorkStatus,
                    OaTime = oaList[len - 1].OaTime,
                    OaAcc = (oaList.Sum(o => o.OaAcc) - oaList.Max<DataOa>(o => o.OaAcc) - oaList.Min<DataOa>(o => o.OaAcc)) / (len - 2),
                    OaBg = (oaList.Sum(o => o.OaBg) - oaList.Max<DataOa>(o => o.OaBg) - oaList.Min<DataOa>(o => o.OaBg)) / (len - 2),
                    OaEnv = (oaList.Sum(o => o.OaEnv) - oaList.Max<DataOa>(o => o.OaEnv) - oaList.Min<DataOa>(o => o.OaEnv)) / (len - 2),
                    OaVel = (oaList.Sum(o => o.OaVel) - oaList.Max<DataOa>(o => o.OaVel) - oaList.Min<DataOa>(o => o.OaVel)) / (len - 2),
                    OaBv = (oaList.Sum(o => o.OaBv) - oaList.Max<DataOa>(o => o.OaBv) - oaList.Min<DataOa>(o => o.OaBv)) / (len - 2),
                    OaKurt = (oaList.Sum(o => o.OaKurt) - oaList.Max<DataOa>(o => o.OaKurt) - oaList.Min<DataOa>(o => o.OaKurt)) / (len - 2),
                    OaCF = (oaList.Sum(o => o.OaCF) - oaList.Max<DataOa>(o => o.OaCF) - oaList.Min<DataOa>(o => o.OaCF)) / (len - 2),
                    OaDisp = (oaList.Sum(o => o.OaDisp) - oaList.Max<DataOa>(o => o.OaDisp) - oaList.Min<DataOa>(o => o.OaDisp)) / (len - 2),
                    OaTemp = (oaList.Sum(o => o.OaTemp) - oaList.Max<DataOa>(o => o.OaTemp) - oaList.Min<DataOa>(o => o.OaTemp)) / (len - 2),
                };
                IDataOaMonthService dataOaMonthService = Program.container.Resolve<IDataOaMonthService>();
                dataOaMonthService.InsertEntity(oa);
            }
            else
            {
                DataOaYear oa = new DataOaYear()
                {
                    AreaId = oaList[0].AreaId,
                    McId = oaList[0].McId,
                    ParId = oaList[0].ParId,
                    DirId = oaList[0].DirId,
                    OaWorkStatus = oaList[0].OaWorkStatus,
                    OaTime = oaList[len - 1].OaTime,
                    OaAcc = (oaList.Sum(o => o.OaAcc) - oaList.Max<DataOa>(o => o.OaAcc) - oaList.Min<DataOa>(o => o.OaAcc)) / (len - 2),
                    OaBg = (oaList.Sum(o => o.OaBg) - oaList.Max<DataOa>(o => o.OaBg) - oaList.Min<DataOa>(o => o.OaBg)) / (len - 2),
                    OaEnv = (oaList.Sum(o => o.OaEnv) - oaList.Max<DataOa>(o => o.OaEnv) - oaList.Min<DataOa>(o => o.OaEnv)) / (len - 2),
                    OaVel = (oaList.Sum(o => o.OaVel) - oaList.Max<DataOa>(o => o.OaVel) - oaList.Min<DataOa>(o => o.OaVel)) / (len - 2),
                    OaBv = (oaList.Sum(o => o.OaBv) - oaList.Max<DataOa>(o => o.OaBv) - oaList.Min<DataOa>(o => o.OaBv)) / (len - 2),
                    OaKurt = (oaList.Sum(o => o.OaKurt) - oaList.Max<DataOa>(o => o.OaKurt) - oaList.Min<DataOa>(o => o.OaKurt)) / (len - 2),
                    OaCF = (oaList.Sum(o => o.OaCF) - oaList.Max<DataOa>(o => o.OaCF) - oaList.Min<DataOa>(o => o.OaCF)) / (len - 2),
                    OaDisp = (oaList.Sum(o => o.OaDisp) - oaList.Max<DataOa>(o => o.OaDisp) - oaList.Min<DataOa>(o => o.OaDisp)) / (len - 2),
                    OaTemp = (oaList.Sum(o => o.OaTemp) - oaList.Max<DataOa>(o => o.OaTemp) - oaList.Min<DataOa>(o => o.OaTemp)) / (len - 2),
                };
                IDataOaYearService dataOaYearService = Program.container.Resolve<IDataOaYearService>();
                dataOaYearService.InsertEntity(oa);
            }
        }
    }

    /// <summary>
    /// 队列数据
    /// </summary>
    public class DataOaQueue
    {
        public DataOaQueue()
        {
            DataQueue = new Dictionary<string, Queue<DataOa>>();
            DilutedCount = new Dictionary<string, int>();
        }

        /// <summary>
        /// 稀释次数
        /// </summary>
        public Dictionary<string,int> DilutedCount { get; set; }

        /// <summary>
        /// 队列数据
        /// </summary>
        public Dictionary<string, Queue<DataOa>> DataQueue { get; set; }
    }

    /// <summary>
    /// 报警次数
    /// </summary>
    public class AlarmTimes
    {
        private Dictionary<string, int> _alarm1 = new Dictionary<string, int>();
        private Dictionary<string, int> _alarm2 = new Dictionary<string, int>();

        public Dictionary<string, int> Alarm1 { get { return _alarm1; } }
        public Dictionary<string, int> Alarm2 { get { return _alarm2; } }
        public AlarmTimes()
        {
            _alarm1.Add("Acc", 0);
            _alarm1.Add("Vel", 0);
            _alarm1.Add("Disp", 0);
            _alarm1.Add("Env", 0);
            _alarm1.Add("Bg", 0);
            _alarm1.Add("Bv", 0);
            _alarm1.Add("CF", 0);
            _alarm1.Add("Kurt", 0);
            _alarm1.Add("Temp", 0);

            _alarm2.Add("Acc", 0);
            _alarm2.Add("Vel", 0);
            _alarm2.Add("Disp", 0);
            _alarm2.Add("Env", 0);
            _alarm2.Add("Bg", 0);
            _alarm2.Add("Bv", 0);
            _alarm2.Add("CF", 0);
            _alarm2.Add("Kurt", 0);
            _alarm2.Add("Temp", 0);

        }

        /// <summary>
        /// 一级报警次数增加
        /// </summary>
        /// <param name="key"></param>
        public void AddAlarm1(string key)
        {
            _alarm1[key]++;
        }

        /// <summary>
        /// 二级报警次数增加
        /// </summary>
        /// <param name="key"></param>
        public void AddAlarm2(string key)
        {
            _alarm2[key]++;
        }

        /// <summary>
        /// 获取最大的一级报警次数
        /// </summary>
        /// <returns></returns>
        public int GetAlarm1Times()
        {
            return _alarm1.Max(d => d.Value);
        }

        /// <summary>
        /// 获取最大的二级报警次数
        /// </summary>
        /// <returns></returns>
        public int GetAlarm2Times()
        {
            return _alarm2.Max(d => d.Value);
        }

    }

}
