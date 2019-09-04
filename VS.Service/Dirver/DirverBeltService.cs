using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverBeltService : BaseService<DirverBelt>, IDirverBeltService
	{
		private IDirverBeltDal _dirverBeltDal { get; set; }
		public DirverBeltService(IDirverBeltDal dirverBeltDal)
		{
			_dirverBeltDal = dirverBeltDal;
			BaseDal = _dirverBeltDal;
		}


	}
}
