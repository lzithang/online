using SqlSugar;

namespace Model

{
	[SugarTable("tb_malfunction_template")]
	public class MalfunctionTemplate
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="mt_id")]
		public int MtId { get; set; }

		/// </summary>
		/// 模板名称
		/// </summary>
		[SugarColumn(ColumnName ="mt_name")]
		public string MtName { get; set; }

		/// </summary>
		/// 所属元件类型
		/// </summary>
		[SugarColumn(ColumnName ="unit_type")]
		public int UnitType { get; set; }

		/// </summary>
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName ="remark")]
		public string Remark { get; set; }

	}
}
