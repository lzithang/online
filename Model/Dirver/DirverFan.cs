using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_fan")]
	public class DirverFan
	{
		/// </summary>
		/// 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="f_id")]
		public int FId { get; set; }

		/// </summary>
		/// 描述
		/// </summary>
		[SugarColumn(ColumnName ="f_name")]
		public string FName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="f_mark")]
		public string FMark { get; set; }

		/// </summary>
		/// 风机叶片数
		/// </summary>
		[SugarColumn(ColumnName ="f_blades")]
		public int FBlades { get; set; }

		/// </summary>
		/// 叶片通过频率
		/// </summary>
		[SugarColumn(ColumnName ="f_blades_frequecy")]
		public float FBladesFrequecy { get; set; }

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

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="f_diffuser_vane")]
		public int FDiffuserVane { get; set; }

	}
}
