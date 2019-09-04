using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverShiftingService : BaseService<DirverShifting>, IDirverShiftingService
	{
		private IDirverShiftingDal _dirverShiftingDal { get; set; }
		public DirverShiftingService(IDirverShiftingDal dirverShiftingDal)
		{
			_dirverShiftingDal = dirverShiftingDal;
			BaseDal = _dirverShiftingDal;
		}


	}
}
