using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class ParameterService : BaseService<Parameter>, IParameterService
	{
		private IParameterDal _parameterDal { get; set; }
		public ParameterService(IParameterDal parameterDal)
		{
			_parameterDal = parameterDal;
			BaseDal = _parameterDal;
		}


	}
}
