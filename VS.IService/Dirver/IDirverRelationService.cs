using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IService
{
	public interface IDirverRelationService:IBaseService<DirverRelation>
	{
        /// <summary>
        /// ��ȡ����Ԫ��
        /// </summary>
        /// <returns></returns>
        Dictionary<int, object> GetUnitAll(int areaId, int mcId);
    }
}
