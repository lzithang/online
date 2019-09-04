using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;

namespace VS.IRepository
{
    public interface IDataOaDal : IBaseRepository<DataOa>
    {
        /// <summary>
        /// 获取方向下最新 count条数据
        /// </summary>
        /// <param name="ids">方向</param>
        /// <param name="count">数量</param>
        /// <returns>结果集</returns>
        List<DataOa> GetDataOaByDirId(IdsModel ids, int count);

        /// <summary>
        /// 获取所有方向下最新一笔数据
        /// </summary>
        /// <returns>结果集</returns>
        List<DataOa> GetDataOaNew();

        /// <summary>
        /// 获取机器下所有方向最新一笔数据
        /// </summary>
        /// <returns>结果集</returns>
        List<DataOa> GetDataOaNewByMcId(int areaId, int mcId);

        /// <summary>
        /// 获取方向下所有总值，每一项是个数组
        /// </summary>
        /// <param name="ids">方向</param>
        /// <returns></returns>
        object GetDataOaByDirId(IdsModel ids);

        /// <summary>
        /// 查询方向下的时间数据列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TimeModel> GetDataOaTimeList(IdsModel ids);
    }
}
