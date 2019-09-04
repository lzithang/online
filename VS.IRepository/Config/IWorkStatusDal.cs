using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IWorkStatusDal:IBaseRepository<WorkStatus>
	{
        /// <summary>   
        /// 根据通道Id获取对应的工作状态
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        List<WorkStatus> GetWorkStatusByDirId(int areaId, int channelId);

    }
}
