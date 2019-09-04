using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_relation")]
	public class DirverRelation
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="dr_id")]
		public int DrId { get; set; }

		/// </summary>
		/// 输入转速
		/// </summary>
		[SugarColumn(ColumnName ="input")]
		public float Input { get; set; }

		/// </summary>
		/// 输出转速
		/// </summary>
		[SugarColumn(ColumnName ="output")]
		public float Output { get; set; }

		/// </summary>
		/// json数据（计算结果）
		/// </summary>
		[SugarColumn(ColumnName ="data")]
		public string Data { get; set; }

		/// </summary>
		/// 父级Id
		/// </summary>
		[SugarColumn(ColumnName ="parentId")]
		public int ParentId { get; set; }

		/// </summary>
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName ="remark")]
		public string Remark { get; set; }

		/// </summary>
		/// 区域Id
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 机器Id
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 类型Id 说明:1.电动机;2.轴承;3.齿轮;4.风机;5.泵;6.皮带传动;7.变速结构;8.线速度;9.行星齿轮;10.轴频率;
		/// </summary>
		[SugarColumn(ColumnName ="d_type")]
		public int DType { get; set; }

		/// </summary>
		/// 驱动Id
		/// </summary>
		[SugarColumn(ColumnName ="d_id")]
		public int DId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="du_id")]
		public int DuId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dr_state")]
		public int DrState { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dr_sort")]
		public int DrSort { get; set; }

	}
}
