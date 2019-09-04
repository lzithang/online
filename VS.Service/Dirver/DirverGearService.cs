using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverGearService : BaseService<DirverGear>, IDirverGearService
	{
		private IDirverGearDal _dirverGearDal { get; set; }
		public DirverGearService(IDirverGearDal dirverGearDal)
		{
			_dirverGearDal = dirverGearDal;
			BaseDal = _dirverGearDal;
		}


	}
}
