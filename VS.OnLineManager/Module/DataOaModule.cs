using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Common;
using VS.IService;

namespace VS.OnLineManager
{
    public class DataOaModule:IDataOaModule
    {
        private IDataOaService _dataOaService;
        private IMachineStopService _machineStopService;
        private IMeterageService _meterageService;

        private IOaAlarmModule _oaAlarmModule;
        private IStopModule _stopModule;

        private List<MachineStop> _machineStopList;
       
        public DataOaModule(IDataOaService dataOaService, IMeterageService meterageService,IOaAlarmModule oaAlarmModule,IStopModule stopModule, IMachineStopService machineStopService)
        {
            _dataOaService = dataOaService;
            _meterageService = meterageService;
            _machineStopService = machineStopService;
            _oaAlarmModule = oaAlarmModule;
            _stopModule = stopModule;


        }

        public void GetDataOa(SiteModel site, SocketMiddleware socket)
        {
            Console.WriteLine("开始采集总值数据！");
            _machineStopList = _machineStopService.Query(ms => ms.AreaId == site.AearId);

            try
            {
                //通道OA的数据集合        
                List<AutoMeasuredValue> amvList = new List<AutoMeasuredValue>();
                //工况集合
                List<int> workStatuesList = null;
                //获取接收的数据缓存
                byte[] sendData = null;
                #region 准备获取总值

                //PrintHelper.Info("【{0}】发送OA采集命令成功！{1}", _site.GetSN(), sendData);
                /********************************************************************************************************************************
                 * 【1 |	0x??】
                 * 【2，3，4，5 |	P，P，O，K  ASCII码等于(80，80，79，75)】
                 * 【6，7 | 仪器序列号(1~65536)（0填充）】
                 * 【8,9,10,11 |	仪器报警状态100：正常；1~17：不同报警】
                 * 【12,13,14,15 |	(int数据)振动总值参数检测的次数AutoRun_Num（用于在结构体里面提取有效振动总值参数的个数）】
                 * 【16,17,18,19 |	(int数据)一个测点的振动总值参数占用字节的位数84*AutoRun_Num（运行的次数） + 20】
                 * 【20,21,22,23 |	(int数据)当前运行的测点组号Num为0：准备好非0：等待Num*10后再发此命令查询】
                 * 【24 | 校验】
                 *******************************************************************************************************************************/
                #endregion

                #region 总值获取

                //循环获取每个通道下的数据
                foreach (ChannelStruct channel in site.ChannelStructList)
                {
                    workStatuesList = channel.StateStatus.Where(s => s != -1).ToList();
                    sendData = OrderHelper.GetDataOaByChannelNum(channel.ChannelID, false);
                    if (socket.Send(sendData))
                    {
                        byte[] dataTemp = new byte[13];
                        if (socket.Receive(dataTemp))
                        {
                            int count = BitConverter.ToInt32(dataTemp, 5);
                            if (count > 0)
                            {
                                if (socket.Send(OrderHelper.GetDataOaByChannelNum(channel.ChannelID, true)))
                                {
                                    int length = 28 + count * 84;
                                    byte[] revData = new byte[length];
                                    if (socket.Receive(revData))
                                    {
                                        //获取数据进行结构化转换
                                        AutoMeasuredValue amv = GetAutoMeasuredValue(revData);
                                        //工况筛选
                                        IEnumerable<MeasuredValue> measuredValueList = amv.MeasuredList.Where(m => workStatuesList.Exists(w => w == m.WorkStatus));
                                        amv.MeasuredList = measuredValueList == null ? new List<MeasuredValue>() : measuredValueList.ToList();
                                        amvList.Add(amv);
                                    }
                                }

                            }
                        }
                    }
                }
                //通道采集完成后，告诉下位机继续自动运行
                sendData = OrderHelper.GetDataOaByChannelNum(-1, false);
                if (socket.Send(sendData))
                {
                    byte[] revData = new byte[13];
                    if (socket.Receive(revData))
                    {
                        if (OrderHelper.CheckCode(revData, 0) == revData[revData.Length - 1]
                            && revData[1] == (byte)'P' && revData[2] == (byte)'P')
                        {
                            if (revData[3] == (byte)'O' && revData[4] == (byte)'K')
                            {
                                //PrintHelper.Info("将站点置为自动运行模式!");
                            }
                        }
                    }
                }
                #endregion

                #region 总值存储  同时修改报警状态
                //存储总值
                if (amvList.Count > 0)
                {
                    foreach (AutoMeasuredValue amv in amvList)
                    {
                        if (amv == null)
                            continue;
                        Console.WriteLine("【区域：{0}，机器：{1}，测点：{2}，方向：{3}一共有{4}笔数据】", amv.areaId, amv.machineId, amv.monitorId, amv.position_HVA, amv.MeasuredList.Count);
                        InserDataOaList(amv);

                    }
                    
                }
                #endregion

                Console.WriteLine("采集总值数据完成！");
            }
            catch (Exception ex)
            {
                Program.LoggerHelper.Error(typeof(DataOaModule), ex.Message, ex);
                Console.WriteLine(string.Format("采集总值数据出现异常！{0}", ex));
            }
        }

        private AutoMeasuredValue GetAutoMeasuredValue(byte[] data)
        {
            if (data.Length == 28)
                return null;

            AutoMeasuredValue amv = new AutoMeasuredValue();
            int nYear = 0;
            int nMonth = 0;
            int nDay = 0;
            int nHour = 0;
            int nMinute = 0;
            int nSecond = 0;
            try
            {
                amv.areaId = BitConverter.ToInt32(data, 0);
                amv.machineId = BitConverter.ToInt32(data, 4);
                amv.monitorId = BitConverter.ToInt32(data, 8);
                amv.position_HVA = BitConverter.ToInt32(data, 12);
                amv.channeld = BitConverter.ToInt32(data, 16);
                amv.monitorPeriodTime = BitConverter.ToInt32(data, 20);
                amv.typeSensor = BitConverter.ToInt32(data, 24);

                if (amv.areaId == 0)//数据无效 返回空
                    return null;


                amv.MeasuredList = new List<MeasuredValue>();
                //获取运行次数 =（总长度-数据头）/ 结构体大小
                int runNum = (data.Length - 28) / 84;
                for (int i = 0; i < runNum; i++)
                {
                    MeasuredValue mv = new MeasuredValue();
                    mv = (MeasuredValue)MarshalHelper.ByteToStruct(data, i * 84 + 28, mv.GetType(), 84);
                    nYear = mv.year; nMonth = mv.month; nDay = mv.day; nHour = mv.hour; nMinute = mv.minute; nSecond = mv.second;
                    DateTime dateTime = new DateTime(mv.year, mv.month, mv.day, mv.hour, mv.minute, mv.second);
                    //PrintHelper.Info("CH-{0:D2} 数据{1} -- {2} ", channeld, i + 1, dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    bool IsExists = amv.MeasuredList.Exists(x => x.year == mv.year && x.month == mv.month
                     && x.day == mv.day && x.hour == mv.hour && x.minute == mv.minute && x.second == mv.second);
                    if (!IsExists)
                    {
                        amv.MeasuredList.Add(mv);
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(System.Reflection.MethodBase.GetCurrentMethod(), ex.Message);
                return null;
            }
            return amv;
        }

        /// <summary>
        /// 插入总值
        /// </summary>
        /// <param name="amv"></param>
        private void InserDataOaList(AutoMeasuredValue amv)
        {
            List<DataOa> list = new List<DataOa>();
            Meterage meterage = _meterageService.Query(m => m.AreaId == amv.areaId && m.McId == amv.machineId && m.ParId == amv.monitorId && m.DirId == amv.position_HVA).FirstOrDefault();

            foreach (var measuredValue in amv.MeasuredList)
            {
                DataOa oAData = new DataOa()
                {
                    AreaId = amv.areaId,
                    McId = amv.machineId,
                    ParId = amv.monitorId,
                    DirId = amv.position_HVA
                };
                //获取采集时间
                oAData.OaTime = new DateTime(measuredValue.year, measuredValue.month, measuredValue.day,
                    measuredValue.hour, measuredValue.minute, measuredValue.second);
                if (!float.IsNaN(measuredValue.rms_Acc_F))
                    oAData.OaAcc = measuredValue.rms_Acc_F;
                if (!float.IsNaN(measuredValue.Disp))
                    oAData.OaDisp = measuredValue.Disp;
                if (!float.IsNaN(measuredValue.CF_Acc))
                    oAData.OaCF = measuredValue.CF_Acc;
                if (!float.IsNaN(measuredValue.Kurtosis_Acc))
                    oAData.OaKurt = measuredValue.Kurtosis_Acc;
                if (!float.IsNaN(measuredValue.rms_Vel_BP))
                    oAData.OaVel = measuredValue.rms_Vel_BP;
                if (!float.IsNaN(measuredValue.rms_Bg_HP))
                    oAData.OaBg = measuredValue.rms_Bg_HP;
                if (!float.IsNaN(measuredValue.rms_Bv_HP))
                    oAData.OaBv = measuredValue.rms_Bv_HP;
                if (!float.IsNaN(measuredValue.rms_Env))
                    oAData.OaEnv = measuredValue.rms_Env;
                oAData.OaTacho = measuredValue.Speed;
                if (!float.IsNaN(measuredValue.SensorStaticValue))
                    oAData.OaTemp = measuredValue.SensorStaticValue;
                oAData.OaWorkStatus = measuredValue.WorkStatus;
                oAData.OaAccType = "rms";
                oAData.OaBgType = "rms";
                oAData.OaBvType = "rms";
                oAData.OaDispType = "rms";
                oAData.OaEnvType = "rms";
                oAData. OaVelType = "rms";
                oAData.OaTempType = "gz1";

                list.Add(oAData);
                Console.WriteLine(string.Format("总值数据BG:{0}, BV:{1}, ENV:{2}, DISP:{3}, VEL:{4}, ACC:{5}, CF:{6},KURT:{7},Temp:{8} ", oAData.OaBg, oAData.OaBv, oAData.OaEnv, oAData.OaDisp, oAData.OaVel, oAData.OaAcc, oAData.OaCF, oAData.OaKurt,oAData.OaTemp));
            }

            //验证是否停机状态 报警验证及添加数据
            if (!_stopModule.ValidateStop(list, meterage, _machineStopList))
            {
                _dataOaService.InsertEntityList(list);
                _oaAlarmModule.ValidateAlarm(list, meterage);
                _oaAlarmModule.SaveCache();
            }
            
        }
       


    }
}
