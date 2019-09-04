using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class FactoryService : BaseService<Factory>, IFactoryService
	{
		private IFactoryDal _factoryDal { get; set; }
		public FactoryService(IFactoryDal factoryDal)
		{
			_factoryDal = factoryDal;
			BaseDal = _factoryDal;
		}


	}
}
