using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverBearingService : BaseService<DirverBearing>, IDirverBearingService
	{
		private IDirverBearingDal _dirverBearingDal { get; set; }
		public DirverBearingService(IDirverBearingDal dirverBearingDal)
		{
			_dirverBearingDal = dirverBearingDal;
			BaseDal = _dirverBearingDal;
		}


	}
}
