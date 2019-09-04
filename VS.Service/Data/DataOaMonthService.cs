using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DataOaMonthService : BaseService<DataOaMonth>, IDataOaMonthService
	{
		private IDataOaMonthDal _dataOaMonthDal { get; set; }
		public DataOaMonthService(IDataOaMonthDal dataOaMonthDal)
		{
			_dataOaMonthDal = dataOaMonthDal;
			BaseDal = _dataOaMonthDal;
		}


	}
}
