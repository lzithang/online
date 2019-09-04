using SqlSugar;

namespace Model

{
	[SugarTable("tb_sensor_type")]
	public class SensorType
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="type_id")]
		public int TypeId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="type_name")]
		public string TypeName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="type_tbName")]
		public string TypeTbName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="type_remark")]
		public string TypeRemark { get; set; }

	}
}
