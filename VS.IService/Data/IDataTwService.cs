using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace VS.IService
{
	public interface IDataTwService:IBaseService<DataTw>
	{
        /// <summary>
        /// 获取方向下的时间列表（包含工况）
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="ids">方向</param>
        /// <returns>时间列表(含对应工况)</returns>
        ResultData GetDataTwTimeList(string parameter, IdsModel ids);

        /// <summary>
        /// 获取方向下某时间波形
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="ids">方向</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        ResultData GetDataTwByDirId(string parameter, IdsModel ids, TimeModel time);

        /// <summary>
        /// 插入波形
        /// </summary>
        /// <param name="dataTw"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool InsertDataTw(DataTw dataTw, string tableName);
    }
}
