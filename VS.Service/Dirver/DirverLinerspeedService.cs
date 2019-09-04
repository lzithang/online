using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverLinerspeedService : BaseService<DirverLinerspeed>, IDirverLinerspeedService
	{
		private IDirverLinerspeedDal _dirverLinerspeedDal { get; set; }
		public DirverLinerspeedService(IDirverLinerspeedDal dirverLinerspeedDal)
		{
			_dirverLinerspeedDal = dirverLinerspeedDal;
			BaseDal = _dirverLinerspeedDal;
		}


	}
}
