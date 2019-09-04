using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_fluidcoupling")]
	public class DirverFluidcoupling
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="fc_id")]
		public int FcId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="fc_name")]
		public string FcName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="fc_mark")]
		public string FcMark { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="fc_range1")]
		public float FcRange1 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="fc_range2")]
		public float FcRange2 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="fc_range3")]
		public float FcRange3 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_ids")]
		public string ParIds { get; set; }

	}
}
