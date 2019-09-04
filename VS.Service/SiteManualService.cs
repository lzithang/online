using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SiteManualService : BaseService<SiteManual>, ISiteManualService
	{
		private ISiteManualDal _siteManualDal { get; set; }
		public SiteManualService(ISiteManualDal siteManualDal)
		{
			_siteManualDal = siteManualDal;
			BaseDal = _siteManualDal;
		}


	}
}
