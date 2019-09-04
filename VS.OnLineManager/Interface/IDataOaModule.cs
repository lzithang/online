using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.OnLineManager
{
    public interface IDataOaModule
    {
        void GetDataOa(SiteModel site, SocketMiddleware socket);
    }
}
