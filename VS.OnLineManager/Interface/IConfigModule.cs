using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.OnLineManager
{
    public interface IConfigModule
    {
        SiteModel _site { get; set; }
        SocketMiddleware _sokect { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        void UpdateTime();

        /// <summary>
        /// 更新站点配置
        /// </summary>
        void UpdateSiteConfig();

        /// <summary>
        /// 固件更新
        /// </summary>
        void UpdateSolid();
    }
}
