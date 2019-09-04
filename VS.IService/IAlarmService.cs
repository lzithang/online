using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace VS.IService
{
	public interface IAlarmService:IBaseService<Alarm>
	{
        // <summary>
        /// ��ȡ�����߼���
        /// </summary>
        /// <param name="warningId">����Id</param>
        /// <param name="dangerId">Σ��Id</param>
        /// <returns></returns>
        ResultData GetAlarmList(int warningId, int dangerId);

        /// <summary>
        /// ��ȡ���б�����
        /// </summary>
        /// <returns></returns>
        ResultData GetAlarmListAll();

    }
}
