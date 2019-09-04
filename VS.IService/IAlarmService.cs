using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace VS.IService
{
	public interface IAlarmService:IBaseService<Alarm>
	{
        // <summary>
        /// 获取报警线集合
        /// </summary>
        /// <param name="warningId">警告Id</param>
        /// <param name="dangerId">危险Id</param>
        /// <returns></returns>
        ResultData GetAlarmList(int warningId, int dangerId);

        /// <summary>
        /// 获取所有报警线
        /// </summary>
        /// <returns></returns>
        ResultData GetAlarmListAll();

    }
}
