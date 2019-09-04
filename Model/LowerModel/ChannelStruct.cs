using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 通道结构 266字节
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ChannelStruct
    {
        /// <summary>
        /// 采集站ID，默认0  
        /// </summary>
        public int ModuleID;
        /// <summary>
        /// 区域ID，默认-1
        /// </summary>
        public int AreaID;
        /// <summary>
        /// 区域名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public char[] AreaName;
        /// <summary>
        ///  机器D
        /// </summary>
        public int MachineID;
        /// <summary>
        /// 机器名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public char[] MachineName;
        /// <summary>
        /// 测点的ID
        /// </summary>
        public int MonitorID;
        /// <summary>
        /// 测点名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public char[] MonitorIDName;
        /// <summary>
        /// 默认-1； 通道ID,1~32
        /// </summary>
        public int ChannelID;
        /// <summary>
        /// 通道名称 一个汉字=2个英文字母=2字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public char[] ChannelName;
        /// <summary>
        /// 安装的方向:0水平，1垂直，2轴向，....
        /// </summary>
        public int Position_HVA;
        /// <summary>
        /// 安装的传感器类型：0加速度，1速度，2位移，...
        /// </summary>
        public int TypeSensor;
        /// <summary>
        /// 安装的传感器的灵敏度
        /// </summary>
        public float SensorSensitivity;
        /// <summary>
        /// 传感器偏离最 低 电压值，默认1
        /// </summary>
        public float Bias_limit_low;
        /// <summary>
        /// 传感器偏离最 高 电压值，默认1
        /// </summary>
        public float Bias_limit_high;
        /// <summary>
        /// 传感器稳定时间
        /// </summary>
        public float SensorStableTime;
        /// <summary>
        /// 对应测点的额定转速
        /// </summary>
        public float SpeedRPM;
        /// <summary>
        /// 分配的转速通道号 //0为未设置，1-4为转速信号
        /// </summary>
        public int SpeedChannelNo;
        /// <summary>
        /// 传感器转速比例
        /// </summary>
        public float RpmRatio;
        /// <summary>
        /// 数据有效的最低转速，默认为-1
        /// </summary>
        public float Speed_limit_low;
        /// <summary>
        /// 数据有效的最高转速，默认为-1
        /// </summary>
        public float Speed_limit_high;
        /// <summary>
        /// 转速通道的状态值
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I4)]
        public int[] StateStatus;

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I4)]
        public int[] SpeedSatus;
        /// <summary>
        /// 分析频率 决定采样频率
        /// </summary>
        public int Fmax;
        /// <summary>
        /// 谱线数 决定采样点数
        /// </summary>
        public int Lines;
        /// <summary>
        /// 平均次数 默认1
        /// </summary>
        public int AverageTime;
        /// <summary>
        /// 平均百分比
        /// </summary>
        public float AveragePercent;
        /// <summary>
        /// 默认为1，放大倍数
        /// </summary>
        public float Amplification;
        /// <summary>
        /// 4mA值
        /// </summary>
        public float value4;
        /// <summary>
        /// 20mA值
        /// </summary>
        public float value20;

        /// <summary>
        /// 测量值工程单位名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray,SizeConst = 10)]
        public char[] MeaValueUnit;

        public int FlagRun; //是否运行标志，0不运行 1 运行
    }
}
