using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_bearing")]
	public class DirverBearing
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="Id")]
		public int Id { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="Manufacturer")]
		public string Manufacturer { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="Type")]
		public string Type { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="NumberOfRollers")]
		public int NumberOfRollers { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="Retainer")]
		public float Retainer { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="Roller")]
		public float Roller { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="OuterRing")]
		public float OuterRing { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="InsideRing")]
		public float InsideRing { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="BearingIndex")]
		public int BearingIndex { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="Remarks")]
		public string Remarks { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="IsUserDefine")]
		public int IsUserDefine { get; set; }

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
		[SugarColumn(ColumnName ="name")]
		public string Name { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mark")]
		public string Mark { get; set; }

	}
}
