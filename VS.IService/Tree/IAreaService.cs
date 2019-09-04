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
        /// ��ȡ��˾���������νṹ������
        /// </summary>
        /// <returns></returns>
        ResultData GetTreeAll();

        /// <summary>
        /// ��ȡ·������
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="mcId"></param>
        /// <param name="parId"></param>
        /// <returns></returns>
        PathName GetPathName(int areaId, int mcId, int parId);
    }
}
