using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
    public interface IDirverRelationDal : IBaseRepository<DirverRelation>
    {
        /// <summary>
        /// ��ȡ����Ԫ��
        /// </summary>
        /// <returns></returns>
        Dictionary<int, object> GetUnitAll(int areaId, int mcId);
    }
}
