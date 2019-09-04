using SqlSugar;

namespace Model

{
	[SugarTable("tb_sensor_range")]
	public class SensorRange
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="range_id")]
		public int RangeId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_name")]
		public string RangeName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_value1")]
		public float RangeValue1 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_value2")]
		public float RangeValue2 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_unit")]
		public string RangeUnit { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_remark")]
		public string RangeRemark { get; set; }

	}
}
