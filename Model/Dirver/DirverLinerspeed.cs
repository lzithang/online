using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_linerspeed")]
	public class DirverLinerspeed
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="dl_id")]
		public int DlId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dl_name")]
		public string DlName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dl_mark")]
		public string DlMark { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dl_mps")]
		public float DlMps { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dl_D")]
		public float DlD { get; set; }

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
