using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class WsRelationService : BaseService<WsRelation>, IWsRelationService
	{
		private IWsRelationDal _wsRelationDal { get; set; }
		public WsRelationService(IWsRelationDal wsRelationDal)
		{
			_wsRelationDal = wsRelationDal;
			BaseDal = _wsRelationDal;
		}


	}
}
