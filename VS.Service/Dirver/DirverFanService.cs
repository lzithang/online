using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverFanService : BaseService<DirverFan>, IDirverFanService
	{
		private IDirverFanDal _dirverFanDal { get; set; }
		public DirverFanService(IDirverFanDal dirverFanDal)
		{
			_dirverFanDal = dirverFanDal;
			BaseDal = _dirverFanDal;
		}


	}
}
