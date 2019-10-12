using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [SugarTable("tb_band_diagnosis")]
    public  class BandDiagnosis
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "bd_id",IsIdentity =true,IsPrimaryKey =true)]
        public int BdId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [SugarColumn(ColumnName = "bd_value")]
        public float BdValue { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [SugarColumn(ColumnName = "bd_time")]
        public DateTime BdTime { get; set; }

        /// <summary>
        /// 诊断配置Id
        /// </summary>
        [SugarColumn(ColumnName = "ms_id")]
        public int MsId { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(ColumnName = "bd_unit")]
        public string BdUnit { get; set; }

        /// <summary>
        /// 测量类型 如：（rms,peak,peak-peak)
        /// </summary>
        [SugarColumn(ColumnName = "bd_type")]
        public int BdType { get; set; }

    }
}
