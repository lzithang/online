using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class AlarmRecordService : BaseService<AlarmRecord>, IAlarmRecordService
	{
		private IAlarmRecordDal _alarmRecordDal { get; set; }
		public AlarmRecordService(IAlarmRecordDal alarmRecordDal)
		{
			_alarmRecordDal = alarmRecordDal;
			BaseDal = _alarmRecordDal;
		}


	}
}
