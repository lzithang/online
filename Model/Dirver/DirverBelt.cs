using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_belt")]
	public class DirverBelt
	{
		/// </summary>
		/// 主键id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="b_id")]
		public int BId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="b_name")]
		public string BName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="b_mark")]
		public string BMark { get; set; }

		/// </summary>
		/// 驱动轮
		/// </summary>
		[SugarColumn(ColumnName ="b_d1")]
		public float BD1 { get; set; }

		/// </summary>
		/// 被驱动轮
		/// </summary>
		[SugarColumn(ColumnName ="b_d2")]
		public float BD2 { get; set; }

		/// </summary>
		/// 皮带轮中心距
		/// </summary>
		[SugarColumn(ColumnName ="b_center_length")]
		public float BCenterLength { get; set; }

		/// </summary>
		/// 驱动风机叶片数
		/// </summary>
		[SugarColumn(ColumnName ="b_blades")]
		public int BBlades { get; set; }

		/// </summary>
		/// 输出转速
		/// </summary>
		[SugarColumn(ColumnName ="b_rs")]
		public float BRs { get; set; }

		/// </summary>
		/// 皮带通过频率
		/// </summary>
		[SugarColumn(ColumnName ="b_frequecy")]
		public float BFrequecy { get; set; }

		/// </summary>
		/// 叶片通过频率
		/// </summary>
		[SugarColumn(ColumnName ="b_blades_frequecy")]
		public float BBladesFrequecy { get; set; }

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
