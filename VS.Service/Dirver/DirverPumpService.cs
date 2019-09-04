using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverPumpService : BaseService<DirverPump>, IDirverPumpService
	{
		private IDirverPumpDal _dirverPumpDal { get; set; }
		public DirverPumpService(IDirverPumpDal dirverPumpDal)
		{
			_dirverPumpDal = dirverPumpDal;
			BaseDal = _dirverPumpDal;
		}


	}
}
