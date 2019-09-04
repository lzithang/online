using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class ConnectLogService : BaseService<ConnectLog>, IConnectLogService
	{
		private IConnectLogDal _connectLogDal { get; set; }
		public ConnectLogService(IConnectLogDal connectLogDal)
		{
			_connectLogDal = connectLogDal;
			BaseDal = _connectLogDal;
		}


	}
}
