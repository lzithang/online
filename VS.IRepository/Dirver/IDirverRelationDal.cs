using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
    public interface IDirverRelationDal : IBaseRepository<DirverRelation>
    {
        /// <summary>
        /// 获取所有元件
        /// </summary>
        /// <returns></returns>
        Dictionary<int, object> GetUnitAll(int areaId, int mcId);
    }
}
