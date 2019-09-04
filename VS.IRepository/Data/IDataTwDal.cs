using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;

namespace VS.IRepository
{
    /// <summary>
    /// 波形频谱数据层接口
    /// </summary>
    public interface IDataTwDal : IBaseRepository<DataTw>
    {
        /// <summary>
        /// 获取方向下的时间列表（包含工况）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ids">方向</param>
        /// <returns>时间列表(含对应工况)</returns>
        object GetDataTwTimeList(string tableName, IdsModel ids);

        /// <summary>
        /// 获取方向下某时间波形
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ids">方向</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        List<DataTw> GetDataTwByDirId(string tableName, IdsModel ids, TimeModel time);

        /// <summary>
        /// 插入波形
        /// </summary>
        /// <param name="dataTw"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool InsertDataTw(DataTw dataTw, string tableName);

    }
}
