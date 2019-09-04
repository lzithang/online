using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MeterageRangeService : BaseService<MeterageRange>, IMeterageRangeService
	{
		private IMeterageRangeDal _meterageRangeDal { get; set; }
		public MeterageRangeService(IMeterageRangeDal meterageRangeDal)
		{
			_meterageRangeDal = meterageRangeDal;
			BaseDal = _meterageRangeDal;
		}


	}
}
