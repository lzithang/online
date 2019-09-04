using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IAlarmDal:IBaseRepository<Alarm>
	{
        // <summary>
        /// 获取报警线集合
        /// </summary>
        /// <param name="warningId">警告Id</param>
        /// <param name="dangerId">危险Id</param>
        /// <returns></returns>
        List<Alarm> GetAlarmList(int warningId, int dangerId);

    }
}
