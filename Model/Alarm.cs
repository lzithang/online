using SqlSugar;

namespace Model

{
	[SugarTable("tb_par_alarm")]
	public class Alarm
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="alarm_id")]
		public int AlarmId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_disp")]
		public float AlarmDisp { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_vel")]
		public float AlarmVel { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_bv")]
		public float AlarmBv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_acc")]
		public float AlarmAcc { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_bg")]
		public float AlarmBg { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_env")]
		public float AlarmEnv { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_kurt")]
		public float AlarmKurt { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_CF")]
		public float AlarmCF { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="alarm_t")]
		public float AlarmT { get; set; }

	}
}
