using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_gear")]
	public class DirverGear
	{
		/// </summary>
		/// 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="m_id")]
		public int MId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="m_name")]
		public string MName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="m_mark")]
		public string MMark { get; set; }

		/// </summary>
		/// 主齿轮齿数
		/// </summary>
		[SugarColumn(ColumnName ="m_num1")]
		public int MNum1 { get; set; }

		/// </summary>
		/// 副齿轮1齿数
		/// </summary>
		[SugarColumn(ColumnName ="m_num2")]
		public int MNum2 { get; set; }

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
