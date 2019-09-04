using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SiteModel
    {
        public SiteModel(byte[] buffer)
        {
            string num = BitConverter.ToInt32(buffer, 8).ToString();
            Sn = Convert.ToInt32(num.Substring(num.Length - 3, 3));
            RunState = buffer[7];
            HasOaData = buffer[12] == 1;
            HasTwData = buffer[13] == 1;
            HasLongTwData = buffer[6] == 1;
        }

        /// <summary>
        /// 通道结构
        /// </summary>
        public List<ChannelStruct> ChannelStructList;

        /// <summary>
        /// 站点绑定的参数组
        /// </summary>
        public List<MeterageLibModel> MeterageLibList;

        /// <summary>
        /// 站点工况
        /// </summary>
        public List<WorkStatus> WorkStatusList;

        /// <summary>
        /// 站点编号
        /// </summary>
        public int Sn { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public int AearId { get; set; }

        /// <summary>
        /// （1:空闲；2.断开（下位机不发）；3：监测 4长时间波形采集中）
        /// </summary>
        public int RunState { get; set; }

        /// <summary>
        /// 是否有总值数据
        /// </summary>
        public bool HasOaData { get; set; }

        /// <summary>
        /// 是否有波形数据
        /// </summary>
        public bool HasTwData { get; set; }

        /// <summary>
        /// 是否有长时间波形
        /// </summary>
        public bool HasLongTwData { get; set; }

        /// <summary>
        /// 站点结构更新标记,0 无更新,1时间，2配置，3时间配置，4固件，5时间固件，6配置固件，7时间配置固件
        /// </summary>
        public int IsUpdate { get; set; }

    }
}
