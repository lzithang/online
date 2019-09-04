using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IWorkStatusDal:IBaseRepository<WorkStatus>
	{
        /// <summary>   
        /// ����ͨ��Id��ȡ��Ӧ�Ĺ���״̬
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        List<WorkStatus> GetWorkStatusByDirId(int areaId, int channelId);

    }
}
