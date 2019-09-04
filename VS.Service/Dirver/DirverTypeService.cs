using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverTypeService : BaseService<DirverType>, IDirverTypeService
	{
		private IDirverTypeDal _dirverTypeDal { get; set; }
		public DirverTypeService(IDirverTypeDal dirverTypeDal)
		{
			_dirverTypeDal = dirverTypeDal;
			BaseDal = _dirverTypeDal;
		}


	}
}
