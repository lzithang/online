using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 自动测量值对象
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class AutoMeasuredValue
    {
        /// <summary>
        /// 区域ID，默认-1
        /// </summary>
        public int areaId;

        /// <summary>
        /// 设备ID
        /// </summary>
        public int machineId;

        /// <summary>
        /// 测点的ID
        /// </summary>
        public int monitorId;

        /// <summary>
        /// 默认-1； 通道ID,1~32
        /// </summary>
        public int channeld;

        /// <summary>
        /// 安装的方向:0水平，1垂直，2轴向，....
        /// </summary>
        public int position_HVA;

        /// <summary>
        /// 监测的时间间隔，最小间隔为1分钟
        /// </summary>
        public int monitorPeriodTime;

        /// <summary>
        /// 传感器类型
        /// </summary>
        public int typeSensor;

        /// <summary>
        /// 测量值集合
        /// </summary>
        public List<MeasuredValue> MeasuredList { get; set; }
    }
}
