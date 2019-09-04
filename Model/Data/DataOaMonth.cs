using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [SugarTable("tb_data_oa_month")]
    public class DataOaMonth
    {
        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "oa_id")]
        public int OaId { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_disp")]
        public double OaDisp { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_vel")]
        public double OaVel { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_bv")]
        public double OaBv { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_acc")]
        public double OaAcc { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_bg")]
        public double OaBg { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_env")]
        public double OaEnv { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_kurt")]
        public double OaKurt { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_temp")]
        public double OaTemp { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_CF")]
        public double OaCF { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_time")]
        public DateTime OaTime { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_vel_type")]
        public string OaVelType { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_disp_type")]
        public string OaDispType { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_bv_type")]
        public string OaBvType { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_acc_type")]
        public string OaAccType { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_bg_type")]
        public string OaBgType { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_env_type")]
        public string OaEnvType { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_temp_type")]
        public string OaTempType { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "par_id")]
        public int ParId { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "dir_id")]
        public int DirId { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "mc_id")]
        public int McId { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "area_id")]
        public int AreaId { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_baseLine")]
        public int OaBaseLine { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_state")]
        public int OaState { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_tacho")]
        public double OaTacho { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "oa_work_status")]
        public int OaWorkStatus { get; set; }
    }
}
