using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS.OnLineManager
{
    public enum SiteHandStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Leisure = 1,

        /// <summary>
        /// 检测
        /// </summary>
        Detection = 3,

        /// <summary>
        /// 长时间波形采集
        /// </summary>
        LongTw = 4,

        /// <summary>
        /// 连接出错
        /// </summary>
        Error = -1,

    }
}
