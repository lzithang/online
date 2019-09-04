using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IService
{
	public interface IWorkStatusService:IBaseService<WorkStatus>
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
