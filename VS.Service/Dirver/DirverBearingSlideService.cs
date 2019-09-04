using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverBearingSlideService : BaseService<DirverBearingSlide>, IDirverBearingSlideService
	{
		private IDirverBearingSlideDal _dirverBearingSlideDal { get; set; }
		public DirverBearingSlideService(IDirverBearingSlideDal dirverBearingSlideDal)
		{
			_dirverBearingSlideDal = dirverBearingSlideDal;
			BaseDal = _dirverBearingSlideDal;
		}


	}
}
