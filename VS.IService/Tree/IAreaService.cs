using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;

namespace VS.IService
{
	public interface IAreaService:IBaseService<Area>
	{
        /// <summary>
        /// 获取公司下所有树形结构体数据
        /// </summary>
        /// <returns></returns>
        ResultData GetTreeAll();

        /// <summary>
        /// 获取路径名称
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="mcId"></param>
        /// <param name="parId"></param>
        /// <returns></returns>
        PathName GetPathName(int areaId, int mcId, int parId);
    }
}
