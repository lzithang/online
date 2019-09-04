using SqlSugar;

namespace Model

{
	[SugarTable("tb_meterage_range")]
	public class MeterageRange
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="range_id")]
		public int RangeId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_sampling")]
		public string RangeSampling { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_analyze")]
		public string RangeAnalyze { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_multiple")]
		public int RangeMultiple { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="range_layer")]
		public int RangeLayer { get; set; }

	}
}
