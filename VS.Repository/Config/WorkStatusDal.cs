using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
	public class WorkStatusDal : BaseRepository<WorkStatus>, IWorkStatusDal
	{
        /// <summary>   
        /// 根据通道Id获取对应的工作状态
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public List<WorkStatus> GetWorkStatusByDirId(int areaId, int channelId)
        {
            List<int> list = new List<int>();
            string sql = @"SELECT * FROM tb_work_status WHERE ws_id in (
SELECT ws_id From tb_ws_relation wsr INNER JOIN
(SELECT * FROM tb_config_bind WHERE area_Id = @areaId And channel_Id = @channelId) cb ON 
wsr.area_id = cb.area_Id ANd
wsr.par_id = cb.par_id ANd
wsr.mc_id = cb.mc_id AND
wsr.dir_id = cb.dir_id) ";
           
            return Db.Ado.SqlQuery<WorkStatus>(sql, new { areaId,channelId});
        }
    }
}
