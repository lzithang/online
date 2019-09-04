using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_pump")]
	public class DirverPump
	{
		/// </summary>
		/// 主键id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="p_id")]
		public int PId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="p_name")]
		public string PName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="p_mark")]
		public string PMark { get; set; }

		/// </summary>
		/// 泵流道数
		/// </summary>
		[SugarColumn(ColumnName ="p_vanes")]
		public int PVanes { get; set; }

		/// </summary>
		/// 流道通过频率
		/// </summary>
		[SugarColumn(ColumnName ="p_frequecy")]
		public float PFrequecy { get; set; }

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
