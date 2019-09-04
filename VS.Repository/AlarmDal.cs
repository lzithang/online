using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
    public class AlarmDal : BaseRepository<Alarm>, IAlarmDal
    {
        /// <summary>
        /// 获取报警线集合
        /// </summary>
        /// <param name="warningId">警告Id</param>
        /// <param name="dangerId">危险Id</param>
        /// <returns></returns>
        public List<Alarm> GetAlarmList(int warningId, int dangerId)
        {
            return Db.Queryable<Alarm>().In(a => a.AlarmId, new int[] { warningId, dangerId }).ToList();
        }
    }
}
