using SqlSugar;

namespace Model

{
	[SugarTable("tb_malfunction_parameter")]
	public class MalfunctionParameter
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="mp_id")]
		public int MpId { get; set; }

		/// </summary>
		/// 参数
		/// </summary>
		[SugarColumn(ColumnName ="mp_name")]
		public string MpName { get; set; }

		/// </summary>
		/// 参数说明
		/// </summary>
		[SugarColumn(ColumnName ="remark")]
		public string Remark { get; set; }

		/// </summary>
		/// 所属模板
		/// </summary>
		[SugarColumn(ColumnName ="mt_id")]
		public int MtId { get; set; }

	}
}
