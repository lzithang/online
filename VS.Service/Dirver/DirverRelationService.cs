using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverRelationService : BaseService<DirverRelation>, IDirverRelationService
	{
		private IDirverRelationDal _dirverRelationDal { get; set; }
		public DirverRelationService(IDirverRelationDal dirverRelationDal)
		{
			_dirverRelationDal = dirverRelationDal;
			BaseDal = _dirverRelationDal;
		}


	}
}
