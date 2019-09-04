using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class SensorAccService : BaseService<SensorAcc>, ISensorAccService
	{
		private ISensorAccDal _sensorAccDal { get; set; }
		public SensorAccService(ISensorAccDal sensorAccDal)
		{
			_sensorAccDal = sensorAccDal;
			BaseDal = _sensorAccDal;
		}


	}
}
