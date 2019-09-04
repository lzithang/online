using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_gear_planet")]
	public class DirverGearPlanet
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
		/// 行星齿轮类型1：减速；2：增速；
		/// </summary>
		[SugarColumn(ColumnName ="p_type")]
		public int PType { get; set; }

		/// </summary>
		/// 行星轮数量
		/// </summary>
		[SugarColumn(ColumnName ="p_count")]
		public int PCount { get; set; }

		/// </summary>
		/// 行星轮齿数
		/// </summary>
		[SugarColumn(ColumnName ="p_np")]
		public int PNp { get; set; }

		/// </summary>
		/// 太阳轮齿数
		/// </summary>
		[SugarColumn(ColumnName ="p_sun")]
		public int PSun { get; set; }

		/// </summary>
		/// 太阳轮啮合频率
		/// </summary>
		[SugarColumn(ColumnName ="p_planet")]
		public int PPlanet { get; set; }

		/// </summary>
		/// 齿圈齿数
		/// </summary>
		[SugarColumn(ColumnName ="p_ring")]
		public int PRing { get; set; }

		/// </summary>
		/// 齿轮固有频率 
		/// </summary>
		[SugarColumn(ColumnName ="GearNF")]
		public float GearNF { get; set; }

		/// </summary>
		/// 壳体固有频率
		/// </summary>
		[SugarColumn(ColumnName ="ShellNF")]
		public float ShellNF { get; set; }

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
