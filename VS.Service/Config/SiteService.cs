using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SiteService : BaseService<Site>, ISiteService
	{
		private ISiteDal _siteDal { get; set; }
		public SiteService(ISiteDal siteDal)
		{
			_siteDal = siteDal;
			BaseDal = _siteDal;
		}


	}
}
