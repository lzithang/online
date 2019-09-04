using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;
using VS.Common;
using VS.IRepository;


namespace VS.Repository
{
    public class DataTwDal : BaseRepository<DataTw>, IDataTwDal
    {
        /// <summary>
        /// 获取方向下的时间列表（包含工况）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public object GetDataTwTimeList(string tableName, IdsModel ids)
        {

            string sql = string.Format("SELECT time,data_work_status from `{0}`  WHERE area_id = @AreaId AND mc_id=@McId AND par_id=@ParId AND dir_id=@DirId ORDER BY time DESC", tableName);
            IDataReader reader = Db.Ado.GetDataReader(sql, new { ids.AreaId, ids.McId, ids.ParId, ids.DirId });
            List<long> time = new List<long>();
            List<int> twWorkStatus = new List<int>();
            while (reader.Read())
            {
                time.Add(new DateTimeOffset(Convert.ToDateTime(reader["time"])).ToUnixTimeSeconds());
                twWorkStatus.Add(Convert.ToInt32(reader["data_work_status"]));
            }
            if (!reader.IsClosed)
                reader.Close();
            return new { time, twWorkStatus };
        }

        /// <summary>
        /// 获取方向下某时间波形
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<DataTw> GetDataTwByDirId(string tableName, IdsModel ids, TimeModel time)
        {
            string sql = string.Format("SELECT * FROM `{0}` WHERE  area_id=@AreaId AND mc_id=@McId AND par_id=@ParId AND dir_id=@DirId AND time=@Time", tableName);
            return Db.Ado.SqlQuery<DataTw>(sql, new { ids.AreaId, ids.McId, ids.ParId, ids.DirId, time.Time });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ids"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public object GetDataTw(string tableName, IdsModel ids, int pageIndex, int pageSize,out int total)
        {
            total = 0;
            int start = 0, end = pageSize;
            if (pageIndex > 1)
            {
                start = (pageIndex - 1) * pageSize;
                end = pageIndex * pageSize;
            }
            string sql = string.Format("SELECT SQL_CALC_FOUND_ROWS time,data_work_status from `{0}`  WHERE area_id = @AreaId AND mc_id=@McId AND par_id=@ParId AND dir_id=@DirId ORDER BY time DESC LIMIT {1},{2};SELECT FOUND_ROWS() total;", tableName, start, end);
            IDataReader reader = Db.Ado.GetDataReader(sql, new { ids.AreaId, ids.McId, ids.ParId, ids.DirId });

            List<long> time = new List<long>();
            List<int> twWorkStatus = new List<int>();
            while (reader.Read())
            {
                time.Add(new DateTimeOffset(Convert.ToDateTime(reader["time"])).ToUnixTimeSeconds());
                twWorkStatus.Add(Convert.ToInt32(reader["data_work_status"]));
            }

            if (reader.NextResult())
            {
                if (reader.Read())
                {
                    total = Convert.ToInt32(reader["total"]);
                }
            }
            if (!reader.IsClosed)
                reader.Close();
            return new { time, twWorkStatus };
        }

        /// <summary>
        /// 插入波形
        /// </summary>
        /// <param name="dataTw"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool InsertDataTw(DataTw dataTw,string tableName)
        {
            string sql = $@"INSERT INTO `{tableName}` (data,
                                        time,
                                        area_id,
                                        mc_id,
                                        par_id,
                                        dir_id,
                                        data_state,
                                        data_baseLine,
                                        data_lines,
                                        data_type,
                                        data_points,
                                        data_hz,
                                        data_isFFT,
                                        data_isLongData,
                                        data_format,
                                        data_work_status) VALUES (@Data,
                                        @Time,
                                        @AreaId,
                                        @McId,
                                        @ParId,
                                        @DirId,
                                        @DataState,
                                        @DataBaseLine,
                                        @DataLines,
                                        @DataType,
                                        @DataPoints,
                                        @DataHz,
                                        @DataIsFFT,
                                        @DataIsLongData,
                                        @DataFormat,
                                        @DataWorkStatus)";
            return Db.Ado.ExecuteCommand(sql, new
            {
                dataTw.Data,
                dataTw.Time,
                dataTw.AreaId,
                dataTw.McId,
                dataTw.ParId,
                dataTw.DirId,
                dataTw.DataState,
                dataTw.DataBaseLine,
                dataTw.DataLines,
                dataTw.DataType,
                dataTw.DataPoints,
                dataTw.DataHz,
                dataTw.DataIsFFT,
                dataTw.DataIsLongData,
                dataTw.DataFormat,
                dataTw.DataWorkStatus
            }) > 0;
        }

    }
}
