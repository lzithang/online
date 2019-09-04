using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverMotorService : BaseService<DirverMotor>, IDirverMotorService
	{
		private IDirverMotorDal _dirverMotorDal { get; set; }
		public DirverMotorService(IDirverMotorDal dirverMotorDal)
		{
			_dirverMotorDal = dirverMotorDal;
			BaseDal = _dirverMotorDal;
		}


	}
}
