using SqlSugar;

namespace Model

{
	[SugarTable("tb_meterage")]
	public class Meterage
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="meter_id")]
		public int MeterId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dir_id")]
		public int DirId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_id")]
		public int ParId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_disp")]
		public int MeterDisp { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_vel")]
		public int MeterVel { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_bv")]
		public int MeterBv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_acc")]
		public int MeterAcc { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_bg")]
		public int MeterBg { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_env")]
		public int MeterEnv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_kurt")]
		public int MeterKurt { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_CF")]
		public int MeterCF { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_T")]
		public int MeterT { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_state_oa")]
		public int MeterStateOa { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_State")]
		public int MeterState { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_disp")]
		public float MeterRangeDisp { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_vel")]
		public float MeterRangeVel { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_bv")]
		public float MeterRangeBv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_acc")]
		public float MeterRangeAcc { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_bg")]
		public float MeterRangeBg { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_env")]
		public float MeterRangeEnv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_kurt")]
		public float MeterRangeKurt { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_range_CF")]
		public float MeterRangeCF { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_alarm1")]
		public int MeterAlarm1 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="meter_alarm2")]
		public int MeterAlarm2 { get; set; }

	}
}
