using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MeterageSamplerateService : BaseService<MeterageSamplerate>, IMeterageSamplerateService
	{
		private IMeterageSamplerateDal _meterageSamplerateDal { get; set; }
		public MeterageSamplerateService(IMeterageSamplerateDal meterageSamplerateDal)
		{
			_meterageSamplerateDal = meterageSamplerateDal;
			BaseDal = _meterageSamplerateDal;
		}


	}
}
