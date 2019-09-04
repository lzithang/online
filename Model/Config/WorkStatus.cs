using SqlSugar;

namespace Model

{
	[SugarTable("tb_work_status")]
	public class WorkStatus
	{
		/// </summary>
		/// 主键id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="ws_id")]
		public int WsId { get; set; }

		/// </summary>
		/// 工作状态
		/// </summary>
		[SugarColumn(ColumnName ="work_status")]
		public int WorkStatusValue { get; set; }

		/// </summary>
		/// 工作状态备注
		/// </summary>
		[SugarColumn(ColumnName ="ws_remark")]
		public string WsRemark { get; set; }

		/// </summary>
		/// 区域id
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 0不使用 1使用
		/// </summary>
		[SugarColumn(ColumnName ="isUse")]
		public int IsUse { get; set; }

		/// </summary>
		/// 工况延迟采集时间（秒）
		/// </summary>
		[SugarColumn(ColumnName ="time")]
		public float Time { get; set; }

	}
}
