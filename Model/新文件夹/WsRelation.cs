using SqlSugar;

namespace Model

{
	[SugarTable("tb_ws_relation")]
	public class WsRelation
	{
		/// </summary>
		/// 主键id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="wsr_id")]
		public int WsrId { get; set; }

		/// </summary>
		/// 区域id
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 机器id
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 测点id
		/// </summary>
		[SugarColumn(ColumnName ="par_id")]
		public int ParId { get; set; }

		/// </summary>
		/// 方向id
		/// </summary>
		[SugarColumn(ColumnName ="dir_id")]
		public int DirId { get; set; }

		/// </summary>
		/// 工作状态id
		/// </summary>
		[SugarColumn(ColumnName ="ws_id")]
		public int WsId { get; set; }

	}
}
