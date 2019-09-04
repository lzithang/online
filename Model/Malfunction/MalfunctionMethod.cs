using SqlSugar;

namespace Model

{
	[SugarTable("tb_malfunction_method")]
	public class MalfunctionMethod
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="mm_id")]
		public int MmId { get; set; }

		/// </summary>
		/// 诊断算法名称
		/// </summary>
		[SugarColumn(ColumnName ="mm_name")]
		public string MmName { get; set; }

		/// </summary>
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName ="remark")]
		public string Remark { get; set; }

	}
}
