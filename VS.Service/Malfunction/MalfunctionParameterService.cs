using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MalfunctionParameterService : BaseService<MalfunctionParameter>, IMalfunctionParameterService
	{
		private IMalfunctionParameterDal _malfunctionParameterDal { get; set; }
		public MalfunctionParameterService(IMalfunctionParameterDal malfunctionParameterDal)
		{
			_malfunctionParameterDal = malfunctionParameterDal;
			BaseDal = _malfunctionParameterDal;
		}


	}
}
