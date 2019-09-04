using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SensorRangeService : BaseService<SensorRange>, ISensorRangeService
	{
		private ISensorRangeDal _sensorRangeDal { get; set; }
		public SensorRangeService(ISensorRangeDal sensorRangeDal)
		{
			_sensorRangeDal = sensorRangeDal;
			BaseDal = _sensorRangeDal;
		}


	}
}
