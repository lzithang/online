using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_type")]
	public class DirverType
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="dt_id")]
		public int DtId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dt_name")]
		public string DtName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dt_table")]
		public string DtTable { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dt_mark")]
		public string DtMark { get; set; }

	}
}
