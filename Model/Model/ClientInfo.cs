using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public  class ClientInfo
    {
        /// <summary>
        /// Sn号
        /// </summary>
        public int Sn { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public int CpId { get; set; }

        /// <summary>
        /// 工厂Id
        /// </summary>
        public int FtId { get; set; }

        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FtName { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string Database { get; set; }
    }
}
