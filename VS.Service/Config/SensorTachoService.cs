using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SensorTachoService : BaseService<SensorTacho>, ISensorTachoService
	{
		private ISensorTachoDal _sensorTachoDal { get; set; }
		public SensorTachoService(ISensorTachoDal sensorTachoDal)
		{
			_sensorTachoDal = sensorTachoDal;
			BaseDal = _sensorTachoDal;
		}


	}
}
