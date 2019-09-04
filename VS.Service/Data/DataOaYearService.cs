using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DataOaYearService : BaseService<DataOaYear>, IDataOaYearService
	{
		private IDataOaYearDal _dataOaYearDal { get; set; }
		public DataOaYearService(IDataOaYearDal dataOaYearDal)
		{
			_dataOaYearDal = dataOaYearDal;
			BaseDal = _dataOaYearDal;
		}


	}
}
