using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_roller")]
	public class DirverRoller
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="dr_id")]
		public int DrId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dr_name")]
		public string DrName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dr_mark")]
		public string DrMark { get; set; }

		/// </summary>
		/// 转速
		/// </summary>
		[SugarColumn(ColumnName ="dr_speed")]
		public float DrSpeed { get; set; }

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
