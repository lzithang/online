using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.IService;

namespace VS.OnLineManager
{
    public class DirverHelper
    {
        public List<DirverRelation> _dirverRelationList; //元件关系表
        public Dictionary<int, object> _unitList; //元件集合
        public List<FeatureItem> _featureItemList = new List<FeatureItem>(); //元件特征项
        public MachineRev _machineRev; //当前机器转速
        public void InitDirverData(int areaId, int mcId,IDirverRelationService dirverRelationService,IMachineRevService machineRevService)
        {
            //元件集合及关系
            _dirverRelationList = dirverRelationService.Query(dr => dr.AreaId == areaId && dr.McId == mcId);
            _unitList = dirverRelationService.GetUnitAll(areaId,mcId);
            //机器转速
           _machineRev = machineRevService.Query(dr => dr.AreaId == areaId && dr.McId == mcId).FirstOrDefault();
        }
        /// <summary>
        /// 获取特征值项
        /// </summary>
        /// <param name="dirver">元件关系</param>
        /// <param name="value">同类型元件集合</param>
        /// <param name="nType">元件类型</param>
        /// <param name="nDriverId">元件id</param>
        /// <param name="inRpm">元件输入转速</param>
        /// <returns></returns>
        public FeatureItem GetFeatureItem(DirverRelation dirver, object value, int nType, int nDriverId, int inRpm)
        {
            object obj = null;
            switch (nType)
            {
                case 1: //电动机
                    if (value is List<DirverMotor>)
                    {
                        List<DirverMotor> motors = (List<DirverMotor>)value;
                        if (motors != null && motors.Count > 0)
                        {
                            //集合中查找对应的电机对象
                            obj = motors.Find(x => x.DmId == nDriverId);
                        }
                    }
                    break;
                case 2:
                    if (value is List<DirverGear>)
                    {
                        List<DirverGear> gearList = (List<DirverGear>)value;
                        if (gearList != null && gearList.Count > 0)
                        {
                            obj = gearList.Find(x => x.MId == nDriverId);
                        }
                    }
                    break;
                case 3:
                    #region 风机
                    if (value is List<DirverFan>)
                    {
                        List<DirverFan> fanList = (List<DirverFan>)value;
                        if (fanList != null && fanList.Count > 0)
                        {

                            obj = fanList.Find(x => x.FId == nDriverId);
                        }
                    }

                    #endregion
                    break;
                case 4:
                    #region 泵
                    if (value is List<DirverPump>)
                    {
                        List<DirverPump> pumpList = (List<DirverPump>)value;
                        if (pumpList != null && pumpList.Count > 0)
                        {
                            obj = pumpList.Find(x => x.PId == nDriverId);
                        }
                    }

                    #endregion
                    break;
                case 5:
                    #region 皮带转动/风机
                    if (value is List<DirverBelt>)
                    {
                        List<DirverBelt> beltList = (List<DirverBelt>)value;
                        if (beltList != null && beltList.Count > 0)
                        {
                            obj = beltList.Find(x => x.BId == nDriverId);
                        }
                    }

                    #endregion
                    break;
                case 6:
                    #region 变比机构
                    if (value is List<DirverShifting>)
                    {
                        List<DirverShifting> shiftingList = (List<DirverShifting>)value;
                        if (shiftingList != null && shiftingList.Count > 0)
                        {
                            obj = shiftingList.Find(x => x.DsId == nDriverId);
                        }
                    }

                    #endregion
                    break;
                case 7:
                    #region 线速度
                    if (value is List<DirverLinerspeed>)
                    {
                        List<DirverLinerspeed> linerSpeedList = (List<DirverLinerspeed>)value;
                        if (linerSpeedList != null && linerSpeedList.Count > 0)
                        {
                            obj = linerSpeedList.Find(x => x.DlId == nDriverId);
                        }
                    }
                    #endregion
                    break;
                case 8:
                    #region 行星齿轮箱
                    if (value is List<DirverGearPlanet>)
                    {
                        List<DirverGearPlanet> gearPlanetList = (List<DirverGearPlanet>)value;
                        if (gearPlanetList != null && gearPlanetList.Count > 0)
                        {
                            obj = gearPlanetList.Find(x => x.PId == nDriverId);
                        }
                    }
                    #endregion
                    break;
                case 9:
                    #region 转子/轴
                    if (value is List<DirverRoller>)
                    {
                        List<DirverRoller> rollerList = (List<DirverRoller>)value;
                        if (rollerList != null && rollerList.Count > 0)
                        {
                            obj = rollerList.Find(x => x.DrId == nDriverId);
                        }
                    }

                    #endregion
                    break;
                case 11:
                    #region 滚动轴承
                    if (value is List<DirverBearing>)
                    {
                        List<DirverBearing> bearingList = (List<DirverBearing>)value;
                        if (bearingList != null && bearingList.Count > 0)
                        {
                            obj = bearingList.Find(x => x.Id == nDriverId);
                        }
                    }

                    #endregion
                    break;
                case 12:
                    #region 滑动轴承
                    if (value is List<DirverBearingSlide>)
                    {
                        List<DirverBearingSlide> bearingSlideList = (List<DirverBearingSlide>)value;
                        if (bearingSlideList != null && bearingSlideList.Count > 0)
                        {
                            obj = bearingSlideList.Find(x => x.BsId == nDriverId);
                        }
                    }

                    #endregion
                    break;
                case 13:
                    if (value is List<DirverFluidcoupling>)
                    {
                        List<DirverFluidcoupling> fluidCouplingList = (List<DirverFluidcoupling>)value;
                        if (fluidCouplingList != null && fluidCouplingList.Count > 0)
                        {
                            obj = fluidCouplingList.Find(x => x.FcId == nDriverId);
                        }
                    }
                    break;
            }
            if(inRpm < 0)
            {
                if (_machineRev != null)
                {
                    inRpm = (int)_machineRev.MrRev;
                }
                else
                {
                    inRpm = (int)dirver.Output;
                }
                    
            }
            return GetFeatureItem(nType,obj,inRpm);
        }

        /// <summary>
        /// 获取特征频率
        /// </summary>
        /// <param name="nType">元件类型</param>
        /// <param name="element">元件</param>
        /// <param name="Input">输入转速</param>
        /// <param name="OutPut"></param>
        /// <returns></returns>
        public FeatureItem GetFeatureItem(int nType, object element, float Input)
        {
            if (element == null)
                return null;
            FeatureItem featureItem = null;
            switch (nType)
            {
                case 1:
                    #region 电动机
                    DirverMotor motor = element as DirverMotor;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = Input,
                        FKey = motor.DmId,
                        FName = motor.DmName,
                        FMark = motor.DmMark,
                        FOutSpeedDisplay = motor.DmMark + "_Speed_" + Input,
                        FType = nType,
                        Tag = motor,
                    };
                    if (motor.DmType == 1)
                    {
                        float nRpm2Hz = Input / 60;
                        float n2LF = motor.DmLf * 2;
                        float nSlip = n2LF / motor.DmPoles - nRpm2Hz;
                        float nPPF = nSlip * motor.DmPoles;

                        featureItem["LF"] = motor.DmLf;
                        featureItem["Poles"] = motor.DmPoles;
                        featureItem["RS"] = Input / 60f;
                        featureItem["Bars"] = motor.DmBars;
                        featureItem["Slots"] = motor.DmSlots;
                        featureItem["PPF"] = nPPF;
                        featureItem["RBPF"] = motor.DmBars * nRpm2Hz;
                        featureItem["WSPF"] = motor.DmSlots * nRpm2Hz;
                        featureItem["2xLF"] = n2LF;
                        featureItem["Slip"] = nSlip;
                    }
                    else
                    {
                        featureItem["LF"] = motor.DmLf;
                        featureItem["RS"] = Input / 60f;
                        featureItem["Bar"] = motor.DmBars;
                        featureItem["Brush"] = motor.DmBrush;
                        featureItem["SCR"] = motor.DmScr;
                    }
                    #endregion
                    break;
                case 2:
                    #region 齿轮
                    DirverGear gear = element as DirverGear;
                    float outSpeed = Input * gear.MNum1 / gear.MNum2;
                    featureItem = new FeatureItem()
                    {
                        FOutSpeed = outSpeed,
                        FInSpeed = Input,
                        FKey = gear.MId,
                        FName = gear.MName,
                        FMark = gear.MMark,
                        FOutSpeedDisplay = gear.MMark + "_Speed_" + outSpeed,
                        FType = 2,
                        Tag = gear,
                    };
                    
                    featureItem["Shaft1"] =Input;
                    featureItem["Shaft2"] = outSpeed;
                    featureItem["Z1"] = gear.MNum1;
                    featureItem["Z2"] = gear.MNum2;
                    featureItem["GM"] = Input * gear.MNum1 / 60;
                    #endregion
                    break;
                case 3:
                    #region 风机
                    DirverFan fan = element as DirverFan;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = Input,
                        FKey = fan.FId,
                        FName = fan.FName,
                        FMark = fan.FMark,
                        FOutSpeedDisplay = fan.FMark + "_Speed_" + Input,
                        FType = 3,
                        Tag = fan,
                    };
                    //叶片比问题	BRF = 转子叶片x扩压器叶片xRS/最大公约数	具有旋转叶片和静止扩压叶片的离心机
                    float tempF = fan.FBlades * fan.FDiffuserVane * Input / GCD(fan.FBlades, fan.FDiffuserVane);
                    featureItem["BRF"]= tempF;
                    featureItem["BP"] = fan.FBlades;
                    featureItem["BPF"] = fan.FBlades * Input / 60;
                    featureItem["RS"] = Input / 60f;
                    #endregion
                    break;
                case 4:
                    #region 泵
                    DirverPump pump = element as DirverPump;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = Input,
                        FKey = pump.PId,
                        FName = pump.PName,
                        FMark = pump.PMark,
                        FOutSpeedDisplay = pump.PMark + "_Speed_" + Input,
                        FType = nType,
                        Tag = pump,
                    };
                    featureItem["VP"] = pump.PVanes;
                    featureItem["VPF"] = pump.PVanes * Input / 60f;
                    featureItem["RS"] = Input / 60f;
                    #endregion
                    break;
                case 5:
                    #region 皮带转动/风机
                    DirverBelt belt = element as DirverBelt;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = belt.BD1 * Input / belt.BD2,
                        FKey = belt.BId,
                        FName = belt.BName,
                        FMark = belt.BMark,
                        FOutSpeedDisplay = belt.BMark + "_Speed_" + (belt.BD1 * Input / belt.BD2),
                        FType = nType,
                        Tag = belt
                    };
                    featureItem["D1"] = belt.BD1;
                    featureItem["D2"] = belt.BD2;
                    featureItem["CD"] = belt.BCenterLength;
                    featureItem["BPF"] = (float)((Math.PI * belt.BD1 * Input) / (0.5 * Math.PI * (belt.BD2 - belt.BD1)
                        + 2 * Math.Sqrt(Math.Pow(belt.BCenterLength, 2) + Math.Pow(0.5 * belt.BD2 - 0.5 * belt.BD1, 2))));
                    featureItem["RS"] = belt.BD1 * Input / belt.BD2 / 60f;
                    #endregion
                    break;
                case 6:
                    #region 变速机构
                    DirverShifting shifting = element as DirverShifting;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = Input / shifting.DsRatio,
                        FKey = shifting.DsId,
                        FName = shifting.DsName,
                        FMark = shifting.DsMark,
                        FOutSpeedDisplay = shifting.DsMark + "_Speed_" + (Input / shifting.DsRatio),
                        FType = nType,
                        Tag = shifting,
                     };
                    #endregion
                    break;
                case 7:
                    #region 线速度
                    DirverLinerspeed linerSpeed = element as DirverLinerspeed;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = (float)(linerSpeed.DlMps * 1000 * 60 / (Math.PI * linerSpeed.DlD)),
                        FKey = linerSpeed.DlId,
                        FName = linerSpeed.DlName,
                        FMark = linerSpeed.DlMark,
                        FOutSpeedDisplay = linerSpeed.DlMark + "_Speed_" + (float)(linerSpeed.DlMps * 1000 * 60 / (Math.PI * linerSpeed.DlD)),
                        FType = nType,
                        Tag = linerSpeed,
                    };
                    #endregion
                    break;
                case 8:
                    #region 行星齿轮箱
                    #endregion
                    break;
                case 9:
                    #region 转子/轴
                    DirverRoller roller = element as DirverRoller;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed =Input,
                        FOutSpeed = Input,
                        FKey = roller.DrId,
                        FName = roller.DrName,
                        FMark = roller.DrMark,
                        FOutSpeedDisplay = roller.DrMark + "_Speed_" + Input,
                        FType = nType,
                        Tag = roller,
                    };
                    featureItem["RS"] = Input / 60f;
                    #endregion
                    break;
                case 11:
                    #region 滚动轴承
                    DirverBearing bearing = element as DirverBearing;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = Input,
                        FKey = bearing.Id,
                        FName = bearing.Name,
                        FMark = bearing.Mark,
                        FOutSpeedDisplay = bearing.Mark + "_Speed_" + Input,
                        FType = nType,
                        Tag = bearing,
                        CalcFrequency = new Dictionary<string, float>()
                    };
                    featureItem["RS"] = Input / 60f;
                    featureItem["BPFI"] = bearing.InsideRing;
                    featureItem["BPFO"] = bearing.OuterRing;
                    featureItem["FTF"] = bearing.Retainer;
                    featureItem["BSF"] = bearing.Roller;
                    #endregion
                    break;
                case 12:
                    #region 滑动轴承
                    DirverBearingSlide bearingSlide = element as DirverBearingSlide;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = Input,
                        FKey = bearingSlide.BsId,
                        FName = bearingSlide.BsName,
                        FMark = bearingSlide.BsMark,
                        FOutSpeedDisplay = bearingSlide.BsMark + "_Speed_" + Input,
                        FType = nType,
                        Tag = bearingSlide,
                    };
                    featureItem["RS"] = Input / 60f;
                    #endregion
                    break;
                case 13:
                    DirverFluidcoupling fluidCoupling = element as DirverFluidcoupling;
                    featureItem = new FeatureItem()
                    {
                        FInSpeed = Input,
                        FOutSpeed = Input * fluidCoupling.FcRange3,
                        FKey = fluidCoupling.FcId,
                        FName = fluidCoupling.FcName,
                        FMark = fluidCoupling.FcMark,
                        FOutSpeedDisplay = fluidCoupling.FcMark + "_Speed_" + (Input * fluidCoupling.FcRange3),
                        FType = nType,
                        Tag = fluidCoupling,
                        CalcFrequency = new Dictionary<string, float>()
                    };
                    featureItem["RS"] = Input/ 60f;
                    break;
            }
            return featureItem;
        }

        public void CreateFeatureItemList(int parentId,float rmp)
        {
            //获取一级元素
            List<DirverRelation> dirvers = _dirverRelationList.FindAll(x => x.ParentId == parentId);
            if (dirvers != null && dirvers.Count > 0)
            {
                foreach (DirverRelation dirver in dirvers)
                {
                    //获取同类的设备集合
                    object value = null;
                    if (_unitList.ContainsKey(dirver.DType))
                        value =_unitList[dirver.DType];

                    FeatureItem item = GetFeatureItem(dirver, value, dirver.DType, dirver.DId, (int)rmp);
                    _featureItemList.Add(item);
                    CreateFeatureItemList(dirver.DrId, item.FOutSpeed);
                }
            }
        }

        /// <summary>
        /// 求最大公约数
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        private int GCD(int n1, int n2)
        {
            int temp = Math.Max(n1, n2);
            n2 = Math.Min(n1, n2);//n2中存放两个数中最小的
            n1 = temp;//n1中存放两个数中最大的
            while (n2 != 0)
            {
                n1 = n1 > n2 ? n1 : n2;//使n1中的数大于n2中的数
                int m = n1 % n2;
                n1 = n2;
                n2 = m;
            }
            return n1;
        }

    }
}
