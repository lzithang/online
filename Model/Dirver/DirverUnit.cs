using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_unit")]
	public class DirverUnit
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="du_id")]
		public int DuId { get; set; }

		/// </summary>
		/// 部件名称
		/// </summary>
		[SugarColumn(ColumnName ="du_name")]
		public string DuName { get; set; }

		/// </summary>
		/// 部件标识
		/// </summary>
		[SugarColumn(ColumnName ="du_mark")]
		public string DuMark { get; set; }

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
		[SugarColumn(ColumnName ="du_remark")]
		public string DuRemark { get; set; }

	}
}
