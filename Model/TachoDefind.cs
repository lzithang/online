using SqlSugar;

namespace Model

{
	[SugarTable("tb_tacho_defind")]
	public class TachoDefind
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="td_id")]
		public int TdId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="msr_id")]
		public int MsrId { get; set; }

		/// </summary>
		/// 最小频率范围
		/// </summary>
		[SugarColumn(ColumnName ="td_hz_min")]
		public float TdHzMin { get; set; }

		/// </summary>
		/// 最大频率范围
		/// </summary>
		[SugarColumn(ColumnName ="td_hz_max")]
		public float TdHzMax { get; set; }

		/// </summary>
		/// 最小幅值
		/// </summary>
		[SugarColumn(ColumnName ="td_amp_min")]
		public float TdAmpMin { get; set; }

		/// </summary>
		/// 最大幅值
		/// </summary>
		[SugarColumn(ColumnName ="td_amp_max")]
		public float TdAmpMax { get; set; }

		/// </summary>
		/// 备注字段
		/// </summary>
		[SugarColumn(ColumnName ="td_remark")]
		public string TdRemark { get; set; }

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
		[SugarColumn(ColumnName ="par_id")]
		public int ParId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dir_id")]
		public int DirId { get; set; }

	}
}
