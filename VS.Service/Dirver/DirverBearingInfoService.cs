using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverBearingInfoService : BaseService<DirverBearingInfo>, IDirverBearingInfoService
	{
		private IDirverBearingInfoDal _dirverBearingInfoDal { get; set; }
		public DirverBearingInfoService(IDirverBearingInfoDal dirverBearingInfoDal)
		{
			_dirverBearingInfoDal = dirverBearingInfoDal;
			BaseDal = _dirverBearingInfoDal;
		}


	}
}
