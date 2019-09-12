using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
	public class MalfunctionSettingDal : BaseRepository<MalfunctionSetting>, IMalfunctionSettingDal
	{
        /// <summary>
        /// 根据数据Id 获取诊断项，诊断项为公式
        /// </summary>
        /// <param name="msrId">数据源Id</param>
        /// <returns></returns>
        public List<MalfunctionSetting> GetMalfunctionSettingListByMsrId(int msrId)
        {
            string sql = @"SELECT 
                    ms.ms_id,
                    ms.area_id,
                    ms.mc_id,
                    ms.par_id,
                    ms.dir_id,
                    ms.mt_id,
                    ms.type_id,
                    ms.type_name,
                    ms.msr_id,
                    ms.data_name,
                    ms.unit_name,
                    ms.unit_type,
                    ms.unit_id,
                    mt.center_frequency,
                    mt.sideband_frequency,
                    mt.common_formula,
                    mt.remove_frequency,
                    ms.mm_id,
                    ms.find_type,
                    ms.ms_range,
                    ms.ms_line,
                    ms.easy_warning,
                    ms.warning,
                    ms.danger,
                    ms.remark
                    FROM tb_malfunction_setting ms,tb_malfunction_type mt Where ms.msr_id = @MsrId AND ms.type_id = mt.type_id";
            return Db.Ado.SqlQuery<MalfunctionSetting>(sql,new { MsrId = msrId});
        }
	}
}
