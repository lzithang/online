using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IAreaDal:IBaseRepository<Area>
	{
        /// <summary>
        /// 获取树形结构体数据
        /// </summary>
        /// <returns></returns>
        object GetTreeAll();

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
