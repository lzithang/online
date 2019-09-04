using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MalfunctionMethodService : BaseService<MalfunctionMethod>, IMalfunctionMethodService
	{
		private IMalfunctionMethodDal _malfunctionMethodDal { get; set; }
		public MalfunctionMethodService(IMalfunctionMethodDal malfunctionMethodDal)
		{
			_malfunctionMethodDal = malfunctionMethodDal;
			BaseDal = _malfunctionMethodDal;
		}


	}
}
