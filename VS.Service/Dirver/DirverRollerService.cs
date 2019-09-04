using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverRollerService : BaseService<DirverRoller>, IDirverRollerService
	{
		private IDirverRollerDal _dirverRollerDal { get; set; }
		public DirverRollerService(IDirverRollerDal dirverRollerDal)
		{
			_dirverRollerDal = dirverRollerDal;
			BaseDal = _dirverRollerDal;
		}


	}
}
