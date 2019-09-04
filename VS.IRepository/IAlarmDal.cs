using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IAlarmDal:IBaseRepository<Alarm>
	{
        // <summary>
        /// ��ȡ�����߼���
        /// </summary>
        /// <param name="warningId">����Id</param>
        /// <param name="dangerId">Σ��Id</param>
        /// <returns></returns>
        List<Alarm> GetAlarmList(int warningId, int dangerId);

    }
}
