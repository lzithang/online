using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.OnLineManager
{
    public interface IExtractModule
    {
        /// <summary>
        /// 初始化，处理波形频谱需要的参数信息
        /// </summary>
        /// <param name="site"></param>
        /// <param name="socket"></param>
        void InitConfig(SiteModel site, SocketMiddleware socket);
    }
}
