using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [SugarTable("tb_machine_stop")]
    public class MachineStop
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(ColumnName = "ms_id")]
        public int MsId { get; set; }
        /// </summary>
        /// 测点Id
        /// </summary>
        [SugarColumn(ColumnName = "par_id")]
        public int ParId { get; set; }

        /// </summary>
        /// 方向Id
        /// </summary>
        [SugarColumn(ColumnName = "dir_id")]
        public int DirId { get; set; }

        /// </summary>
        /// 机器Id
        /// </summary>
        [SugarColumn(ColumnName = "mc_id")]
        public int McId { get; set; }

        /// </summary>
        /// 区Id
        /// </summary>
        [SugarColumn(ColumnName = "area_id")]
        public int AreaId { get; set; }

        /// </summary>
        /// 总值类型
        /// </summary>
        [SugarColumn(ColumnName = "ms_type")]
        public string MsType { get; set; }

        /// </summary>
        /// 阈值
        /// </summary>
        [SugarColumn(ColumnName = "ms_value")]
        public float MsValue { get; set; }

        /// </summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remark")]
        public string Remark { get; set; }
    }
}
