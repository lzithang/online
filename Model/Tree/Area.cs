using SqlSugar;

namespace Model

{
	[SugarTable("tb_area")]
	public class Area
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_name")]
		public string AreaName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_principalInfo")]
		public string AreaPrincipalInfo { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_description")]
		public string AreaDescription { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_img_id")]
		public int AreaImgId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="ft_id")]
		public int FtId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="sortOrder")]
		public int SortOrder { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="isActivation")]
		public int IsActivation { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="machineIdMark")]
		public int MachineIdMark { get; set; }

	}
}
