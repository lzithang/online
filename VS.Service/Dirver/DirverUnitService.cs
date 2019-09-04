using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverUnitService : BaseService<DirverUnit>, IDirverUnitService
	{
		private IDirverUnitDal _dirverUnitDal { get; set; }
		public DirverUnitService(IDirverUnitDal dirverUnitDal)
		{
			_dirverUnitDal = dirverUnitDal;
			BaseDal = _dirverUnitDal;
		}


	}
}
