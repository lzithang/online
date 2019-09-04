using SqlSugar;

namespace Model

{
	[SugarTable("tb_sensor_tacho")]
	public class SensorTacho
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="tacho_Id")]
		public int TachoId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="tacho_name")]
		public string TachoName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="tacho_pulseCount")]
		public int TachoPulseCount { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="tacho_IsUSE")]
		public int TachoIsUSE { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="tacho_remark")]
		public string TachoRemark { get; set; }

	}
}
