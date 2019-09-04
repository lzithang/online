using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EmailModel
    {

        /// <summary>
        /// 收件人地址
        /// </summary>
        public string ReceiverAddress { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发件人地址（非必填）
        /// </summary>
        public string SenderAddress { get; set; }
        /// <summary>
        /// 发件人姓名（非必填）
        /// </summary>
        public string SenderName { get; set; }
        /// <summary>
        /// 发件人密码（非必填）
        /// </summary>
        public string SenderPassword { get; set; }

    }
}
