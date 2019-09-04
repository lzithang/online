using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SensorTempService : BaseService<SensorTemp>, ISensorTempService
	{
		private ISensorTempDal _sensorTempDal { get; set; }
		public SensorTempService(ISensorTempDal sensorTempDal)
		{
			_sensorTempDal = sensorTempDal;
			BaseDal = _sensorTempDal;
		}


	}
}
