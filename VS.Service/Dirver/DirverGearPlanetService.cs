using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverGearPlanetService : BaseService<DirverGearPlanet>, IDirverGearPlanetService
	{
		private IDirverGearPlanetDal _dirverGearPlanetDal { get; set; }
		public DirverGearPlanetService(IDirverGearPlanetDal dirverGearPlanetDal)
		{
			_dirverGearPlanetDal = dirverGearPlanetDal;
			BaseDal = _dirverGearPlanetDal;
		}


	}
}
