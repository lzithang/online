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
        /// ��ȡ�����߼���
        /// </summary>
        /// <param name="warningId">����Id</param>
        /// <param name="dangerId">Σ��Id</param>
        /// <returns></returns>
        public List<Alarm> GetAlarmList(int warningId, int dangerId)
        {
            return Db.Queryable<Alarm>().In(a => a.AlarmId, new int[] { warningId, dangerId }).ToList();
        }
    }
}
