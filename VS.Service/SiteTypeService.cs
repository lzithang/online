using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SiteTypeService : BaseService<SiteType>, ISiteTypeService
	{
		private ISiteTypeDal _siteTypeDal { get; set; }
		public SiteTypeService(ISiteTypeDal siteTypeDal)
		{
			_siteTypeDal = siteTypeDal;
			BaseDal = _siteTypeDal;
		}


	}
}
