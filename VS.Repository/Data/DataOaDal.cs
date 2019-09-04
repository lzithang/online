using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;
using VS.Common;
using VS.IRepository;


namespace VS.Repository
{
    public class DataOaDal : BaseRepository<DataOa>, IDataOaDal
    {
        /// <summary>
        /// 获取方向下最新 count条数据
        /// </summary>
        /// <param name="ids">方向</param>
        /// <param name="count">数量</param>
        /// <returns>结果集</returns>
        public List<DataOa> GetDataOaByDirId(IdsModel ids, int count)
        {
            string sql = "SELECT * FROM tb_data_oa WHERE area_id=@AreaId AND mc_id=@McId AND par_id=@ParId AND dir_id=@DirId Order BY oa_time DESC LIMIT 0,@Count";
            return Db.Ado.SqlQuery<DataOa>(sql, new { AreaId = ids.AreaId, McId = ids.McId, ParId = ids.ParId, DirId = ids.DirId, Count = count });
        }

        /// <summary>
        /// 获取所有方向下最新一笔数据
        /// </summary>
        /// <returns>结果集</returns>
        public List<DataOa> GetDataOaNew()
        {
            string sql = "SELECT * FROM (SELECT * FROM tb_data_oa ORDER BY oa_time DESC) oa GROUP BY area_id,mc_id,par_id,dir_id";
            return Db.Ado.SqlQuery<DataOa>(sql);
        }

        /// <summary>
        /// 获取机器下所有方向最新一笔数据
        /// </summary>
        /// <returns>结果集</returns>
        public List<DataOa> GetDataOaNewByMcId(int areaId,int mcId)
        {
            string sql = "SELECT * FROM (SELECT * FROM tb_data_oa WHERE area_id=@areaId AND mc_id=@mcId ORDER BY oa_time DESC) oa GROUP BY area_id,mc_id,par_id,dir_id";
            return Db.Ado.SqlQuery<DataOa>(sql,new {areaId,mcId});
        }

        /// <summary>
        /// 获取方向下所有总值，每一项是个数组
        /// </summary>
        /// <param name="ids">方向</param>
        /// <returns></returns>
        public object GetDataOaByDirId(IdsModel ids)
        {
            string sql = "SELECT * FROM tb_data_oa WHERE area_id=@AreaId AND mc_id=@McId AND par_id=@ParId AND dir_id=@DirId Order BY oa_time DESC";
            IDataReader dataReader =  Db.Ado.GetDataReader(sql, new { AreaId = ids.AreaId, McId = ids.McId, ParId = ids.ParId, DirId = ids.DirId });
            List<float> temp = new List<float>();
            List<float> acc = new List<float>();
            List<float> vel = new List<float>();
            List<float> disp = new List<float>();
            List<float> env = new List<float>();
            List<float> cf = new List<float>();
            List<float> kurt = new List<float>();
            List<float> bv = new List<float>();
            List<float> bg = new List<float>();
            List<long> time = new List<long>();
            List<int> oaWorkStatus = new List<int>();
            while (dataReader.Read())
            {
                temp.Add(Convert.ToInt32(dataReader["oa_temp"]));
                acc.Add(Convert.ToSingle(dataReader["oa_acc"]));
                vel.Add(Convert.ToSingle(dataReader["oa_vel"]));
                bg.Add(Convert.ToSingle(dataReader["oa_bg"]));
                bv.Add(Convert.ToSingle(dataReader["oa_bv"]));
                env.Add(Convert.ToSingle(dataReader["oa_env"]));
                disp.Add(Convert.ToSingle(dataReader["oa_disp"]));
                cf.Add(Convert.ToSingle(dataReader["oa_CF"]));
                kurt.Add(Convert.ToSingle(dataReader["oa_kurt"]));
                time.Add(new DateTimeOffset(Convert.ToDateTime(dataReader["oa_time"])).ToUnixTimeSeconds());
                oaWorkStatus.Add(Convert.ToInt32(dataReader["oa_work_status"]));
            }
            if (!dataReader.IsClosed)
                dataReader.Close();
            
            return new { temp,acc,vel,disp,env,cf,kurt,bv,bg,time,oaWorkStatus};
        }

        /// <summary>
        /// 查询方向下的时间数据列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<TimeModel> GetDataOaTimeList(IdsModel ids)
        {
            string sql = "SELECT time FROM tb_data_oa WHERE area_id=@AreaId AND mc_id=@McId AND par_id=@ParId AND dir_id=@DirId ";
            return Db.Ado.SqlQuery<TimeModel>(sql, ids);
        }

    }
}
