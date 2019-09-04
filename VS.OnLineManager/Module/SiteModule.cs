using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.IService;

namespace VS.OnLineManager
{
    public class SiteModule:ISiteModule
    {
        private ISiteService _siteService;
        private IMeterageLibService _meterageLibService;
        private IConfigBindService _configBindService;
        private ISensorAccService _sensorAccService;
        private ISensorRangeService _sensorRangeService;
        private ISensorTachoService _sensorTachoService;
        private ISensorTempService _sensorTempService;
        private IWorkStatusService _workStatusService;
        private IMapper _mapper;
        public SiteModule(ISiteService siteService,
            IMeterageLibService meterageLibService,
            IConfigBindService configBindService,
            ISensorAccService sensorAccService,
            ISensorRangeService sensorRangeService,
            ISensorTachoService sensorTachoService,
            ISensorTempService sensorTempService,
            IWorkStatusService workStatusService,
            IMapper mapper)
        {
            _configBindService = configBindService;
            _sensorAccService = sensorAccService;
            _sensorRangeService = sensorRangeService;
            _sensorTempService = sensorTempService;
            _sensorTachoService = sensorTachoService;
            _siteService = siteService;
            _meterageLibService = meterageLibService;
            _workStatusService = workStatusService;
            _mapper = mapper;
        }

        public void GetSiteDetails(ref SiteModel site)
        {
            int sn = site.Sn;
            Site siteInfo = _siteService.Query(s => s.SiteSN == sn).FirstOrDefault();
            site.IsUpdate = siteInfo.SiteUpdateTag;
            site.RunState = siteInfo.SiteRunState;
            site.AearId = siteInfo.AreaId;

            List<ConfigBindModel> configBindModelList = _configBindService.GetChannelStructInfo(site.Sn);
            List<ChannelStruct> channelStructList = new List<ChannelStruct>();
            foreach (ConfigBindModel item in configBindModelList)
            {
                ChannelStruct channel = _mapper.Map<ChannelStruct>(item);
                channel.SensorSensitivity = 100;
                channel.Bias_limit_low = 0;
                channel.Bias_limit_high = 0;
                channel.SensorStableTime = -1;
                channel.SpeedRPM = 0;
                channel.SpeedChannelNo = 0;
                channel.RpmRatio = 0;
                channel.Speed_limit_low = -1;
                channel.Speed_limit_high = -1;
                channel.Fmax = -1;
                channel.Lines = 1;
                channel.AverageTime = 1;
                channel.AveragePercent = 1;
                channel.Amplification = 1;
                channel.value4 = -40.0f;
                channel.value20 = 100.0f;
                channel.MeaValueUnit = GetAttrName("");
                switch (channel.TypeSensor)
                {
                    case 0: //加速度
                        SensorAcc accSensor = _sensorAccService.QueryById(item.SensorId);
                        if (accSensor != null)
                        {
                            channel.SensorSensitivity = (float)accSensor.AccSensibility;
                            channel.Bias_limit_low = (float)accSensor.AccBiasLow;
                            channel.Bias_limit_high = (float)accSensor.AccBiasHigh;
                            channel.SensorStableTime = accSensor.AccSettlingTime;
                        }
                        break;
                    case 3://范围
                        SensorRange rangeSensor = _sensorRangeService.QueryById(item.SensorId);
                        if (rangeSensor != null)
                        {
                            channel.value4 = rangeSensor.RangeValue1;
                            channel.value20 = rangeSensor.RangeValue2;
                        }
                        break;
                }
                List<WorkStatus> workStatuesList = _workStatusService.GetWorkStatusByDirId(channel.AreaID,item.ChannelID);
                int[] workStatues = new int[5];

                for (int i = 0; i < 5; i++)
                {
                    workStatues[i] = workStatuesList.Count > i ? workStatuesList[i].WorkStatusValue : -1;

                }
                channel.StateStatus = workStatues;
                channel.SpeedSatus = new int[5];
                channelStructList.Add(channel);
            }
            site.ChannelStructList = channelStructList;
            site.MeterageLibList = _meterageLibService.GetMeterageLibListBySn(site.Sn);
            site.WorkStatusList = _workStatusService.Query(w => w.AreaId == siteInfo.AreaId && w.IsUse == 1);
        }


        private char[] GetAttrName(string strName)
        {
            char[] data = new char[30];
            for (int i = 0; i < data.Length; i++)
                data[i] = ' ';
            char[] temp = strName.ToCharArray();
            Array.Copy(temp, 0, data, 0, temp.Length);
            return data;
        }
    }
}
