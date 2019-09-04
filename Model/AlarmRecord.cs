using SqlSugar;
using System;

namespace Model

{
	[SugarTable("tb_alarm_record")]
	public class AlarmRecord
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="ar_id")]
		public int ArId { get; set; }

		/// </summary>
		/// 区域id
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 机器id
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 测点id
		/// </summary>
		[SugarColumn(ColumnName ="par_id")]
		public int ParId { get; set; }

		/// </summary>
		/// 方向id
		/// </summary>
		[SugarColumn(ColumnName ="dir_id")]
		public int DirId { get; set; }

		/// </summary>
		/// 报警开始时间
		/// </summary>
		[SugarColumn(ColumnName ="begin_time")]
		public DateTime BeginTime { get; set; }

		/// </summary>
		/// 处理时间
		/// </summary>
		[SugarColumn(ColumnName ="end_time")]
		public DateTime EndTime { get; set; }

		/// </summary>
		/// 是否处理状态
		/// </summary>
		[SugarColumn(ColumnName ="alarm_status")]
		public int AlarmStatus { get; set; }

		/// </summary>
		/// 报警级别
		/// </summary>
		[SugarColumn(ColumnName ="alarm_level")]
		public int AlarmLevel { get; set; }

		/// </summary>
		/// 总值位移
		/// </summary>
		[SugarColumn(ColumnName ="oa_disp")]
		public float OaDisp { get; set; }

		/// </summary>
		/// 速度
		/// </summary>
		[SugarColumn(ColumnName ="oa_vel")]
		public float OaVel { get; set; }

		/// </summary>
		/// bv
		/// </summary>
		[SugarColumn(ColumnName ="oa_bv")]
		public float OaBv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="oa_acc")]
		public float OaAcc { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="oa_bg")]
		public float OaBg { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="oa_env")]
		public float OaEnv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="oa_kurt")]
		public float OaKurt { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="oa_temp")]
		public float OaTemp { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="oa_CF")]
		public float OaCF { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm1_disp")]
		public float Alarm1Disp { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm1_vel")]
		public float Alarm1Vel { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm1_bv")]
		public float Alarm1Bv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm1_acc")]
		public float Alarm1Acc { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm1_bg")]
		public float Alarm1Bg { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm1_env")]
		public float Alarm1Env { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm1_kurt")]
		public float Alarm1Kurt { get; set; }

        /// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "alarm1_CF")]
        public float Alarm1CF { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName ="alarm1_temp")]
		public float Alarm1Temp { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_disp")]
		public float Alarm2Disp { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_vel")]
		public float Alarm2Vel { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_bv")]
		public float Alarm2Bv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_acc")]
		public float Alarm2Acc { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_bg")]
		public float Alarm2Bg { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_env")]
		public float Alarm2Env { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_kurt")]
		public float Alarm2Kurt { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm2_temp")]
		public float Alarm2Temp { get; set; }

        /// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "alarm2_CF")]
        public float Alarm2CF { get; set; }

    }
}
