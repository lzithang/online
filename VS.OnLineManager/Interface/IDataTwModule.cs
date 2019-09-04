using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.OnLineManager
{
    public interface IDataTwModule
    {
        void GetDataTw(SiteModel site, SocketMiddleware socket);
    }
}
