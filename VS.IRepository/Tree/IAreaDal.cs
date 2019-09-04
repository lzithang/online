using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IAreaDal:IBaseRepository<Area>
	{
        /// <summary>
        /// ��ȡ���νṹ������
        /// </summary>
        /// <returns></returns>
        object GetTreeAll();

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
