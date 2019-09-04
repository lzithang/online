using Model;
using System;
using System.Collections.Generic;
using System.Text;
using VS.IRepository;
using VS.IService;

namespace VS.Service
{
    public class AlarmService : BaseService<Alarm>, IAlarmService
    {
        private IAlarmDal _alarmDal;
        public AlarmService(IAlarmDal alarmDal)
        {
            BaseDal = alarmDal;
            _alarmDal = alarmDal;
        }

        // <summary>
        /// 获取报警线集合
        /// </summary>
        /// <param name="warningId">警告Id</param>
        /// <param name="dangerId">危险Id</param>
        /// <returns></returns>
        public ResultData GetAlarmList(int warningId, int dangerId)
        {
            return new ResultData(_alarmDal.GetAlarmList(warningId, dangerId));
        }

        /// <summary>
        /// 获取所有报警线
        /// </summary>
        /// <returns></returns>
        public ResultData GetAlarmListAll()
        {
            return new ResultData(_alarmDal.QueryList());
        }


    }
}
