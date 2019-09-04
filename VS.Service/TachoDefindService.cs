using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class TachoDefindService : BaseService<TachoDefind>, ITachoDefindService
	{
		private ITachoDefindDal _tachoDefindDal { get; set; }
		public TachoDefindService(ITachoDefindDal tachoDefindDal)
		{
			_tachoDefindDal = tachoDefindDal;
			BaseDal = _tachoDefindDal;
		}


	}
}
