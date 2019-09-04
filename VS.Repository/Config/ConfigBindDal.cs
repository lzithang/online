using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
	public class ConfigBindDal : BaseRepository<ConfigBind>, IConfigBindDal
	{
        /// <summary>
        /// 获取ChannelStruct类型部分信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public List<ConfigBindModel> GetChannelStructInfo(int sn)
        {
            string sql = $@"SELECT 
            cb.*,
            a.area_name,
            m.mc_name,
            p.par_bearing,
            CASE 
                WHEN mt.dir_id = 1 THEN 'H'  
                WHEN mt.dir_id = 2 THEN 'V'  
                WHEN mt.meter_t= 0 THEN 'A' 
                WHEN mt.meter_t= 1 THEN 'T' END AS dir_name,
            CASE cb.sensorType 	
					            WHEN 0 THEN (SELECT acc_name FROM tb_sensor_acc WHERE acc_id=cb.sensorId) 
					            WHEN 1 THEN (SELECT temp_name FROM tb_sensor_temp WHERE temp_id=cb.sensorId)  
					            WHEN 2 THEN (SELECT tacho_name FROM tb_sensor_tacho WHERE tacho_id=cb.sensorId)  
					            WHEN 3 THEN (SELECT range_name FROM tb_sensor_range WHERE range_id=cb.sensorId) END AS sensor_Name
            FROM (SELECT tb_config_bind.*,s.site_SN 
                                    FROM tb_config_bind 
                                    INNER JOIN (SELECT * FROM tb_site WHERE site_SN = {sn}) s
                                    ON tb_config_bind.site_Id = s.site_Id) AS cb 
            LEFT JOIN tb_area AS a ON cb.area_id=a.area_id 
            LEFT JOIN tb_machine AS m ON cb.area_id=m.area_id AND cb.mc_id=m.mc_id 
            LEFT JOIN tb_parameter AS p ON cb.area_id=p.area_id AND cb.mc_id=p.mc_id AND cb.par_id=p.par_id  
            LEFT JOIN tb_meterage AS mt  ON cb.area_id=mt.area_id AND cb.mc_id=mt.mc_id AND cb.par_id=mt.par_id AND cb.dir_id=mt.dir_id  
            ORDER BY cb.channel_Id ASC";
            List<ConfigBindModel> list = Db.Ado.SqlQuery<ConfigBindModel>(sql);
            return list;
        }
	}
}
