using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SensorTypeService : BaseService<SensorType>, ISensorTypeService
	{
		private ISensorTypeDal _sensorTypeDal { get; set; }
		public SensorTypeService(ISensorTypeDal sensorTypeDal)
		{
			_sensorTypeDal = sensorTypeDal;
			BaseDal = _sensorTypeDal;
		}


	}
}
