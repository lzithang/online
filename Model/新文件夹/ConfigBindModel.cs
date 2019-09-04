using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ConfigBindModel
    {
        /// <summary>
        /// 采集站ID，默认0  
        /// </summary>
        [SugarColumn(ColumnName = "site_Id")]
        public int ModuleID { get; set; }
        /// <summary>
        /// 区域ID，默认-1
        /// </summary>
        [SugarColumn(ColumnName = "area_Id")]
        public int AreaID { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [SugarColumn(ColumnName = "area_name")]
        public string AreaName { get; set; }
        /// <summary>
        ///  机器D
        /// </summary>
        [SugarColumn(ColumnName = "mc_Id")]
        public int MachineID { get; set; }
        /// <summary>
        /// 机器名称
        /// </summary>
        [SugarColumn(ColumnName = "mc_name")]
        public string MachineName { get; set; }
        /// <summary>
        /// 测点的ID
        /// </summary>
        [SugarColumn(ColumnName = "par_Id")]
        public int MonitorID { get; set; }
        /// <summary>
        /// 测点名称
        /// </summary>
        [SugarColumn(ColumnName = "par_bearing")]
        public string MonitorIDName { get; set; }
        /// <summary>
        /// 默认-1； 通道ID,1~32
        /// </summary>
        [SugarColumn(ColumnName = "channel_Id")]
        public int ChannelID { get; set; }
        /// <summary>
        /// 通道名称 一个汉字=2个英文字母=2字节
        /// </summary>
        [SugarColumn(ColumnName = "channel_Id")]
        public string ChannelName { get; set; }
        /// <summary>
        /// 安装的方向:0水平，1垂直，2轴向，....
        /// </summary>
        [SugarColumn(ColumnName = "dir_Id")]
        public int Position_HVA { get; set; }
        /// <summary>
        ///  public int FlagRun; //是否运行标志，0不运行 1 运行
        /// </summary>
        [SugarColumn(ColumnName = "cb_isActivate")]
        public int FlagRun { get; set; }
        /// <summary>
        /// 安装的传感器类型：0加速度，1速度，2位移，...
        /// </summary>
        [SugarColumn(ColumnName = "sensorType")]
        public int TypeSensor { get; set; }
        /// <summary>
        /// 安装的传感器类型：0加速度，1速度，2位移，...
        /// </summary>
        [SugarColumn(ColumnName = "sensorId")]
        public int SensorId { get; set; }
        
    }
}
