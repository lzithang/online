using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.OnLineManager
{
    public interface ISiteModule
    {
        void GetSiteDetails(ref SiteModel site);
    }
}
