using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MeterageService : BaseService<Meterage>, IMeterageService
	{
		private IMeterageDal _meterageDal { get; set; }
		public MeterageService(IMeterageDal meterageDal)
		{
			_meterageDal = meterageDal;
			BaseDal = _meterageDal;
		}


	}
}
