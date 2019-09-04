using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
    public class MeterageLibDal : BaseRepository<MeterageLib>, IMeterageLibDal
    {
        /// <summary>
        /// 根据sn获取测量参数信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public List<MeterageLibModel> GetMeterageLibListBySn(int sn)
        {
            string sql = $@"select ml.*,tb.group_id 
from tb_meterage_lib as ml 
INNER JOIN
		(SELECT DISTINCT ml_id,group_id
		FROM tb_meterage_samplerate AS ms
		WHERE IsSamplerate =1  
		AND  ms.area_id in (SELECT area_id FROM tb_site WHERE site_SN ={sn})) tb 
on tb.ml_id =ml.ml_id";
            return Db.Ado.SqlQuery<MeterageLibModel>(sql);
        }
    }
}
