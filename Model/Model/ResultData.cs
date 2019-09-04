using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 结果数据
    /// </summary>
    public class ResultData
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 结果状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        public ResultData()
        {

        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="isSuccess"></param>
        public ResultData(object data)
        {
            string msg = "get data success!";
            int code = 200;
            bool isSuccess = true;
            if (data == null)
            {
                msg = "no data!";
                code = 400;
                isSuccess = false;
            }
            IsSuccess = isSuccess;
            Data = data;
            Code = code;
            Msg = msg;
        }

    }
}
