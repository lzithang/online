using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// id模型集合
    /// </summary>
    public class IdsModel
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 机器Id
        /// </summary>
        public int McId { get; set; }

        /// <summary>
        /// 测点Id
        /// </summary>
        public int ParId { get; set; }

        /// <summary>
        /// 方向Id
        /// </summary>
        public int DirId { get; set; }
    }
}
