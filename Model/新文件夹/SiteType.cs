using SqlSugar;

namespace Model

{
	[SugarTable("tb_site_type")]
	public class SiteType
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="st_id")]
		public int StId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="st_name")]
		public string StName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="st_StorageSize")]
		public int StStorageSize { get; set; }

		/// </summary>
		/// 通道数
		/// </summary>
		[SugarColumn(ColumnName ="st_ChannelNum")]
		public int StChannelNum { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="st_groupNum")]
		public int StGroupNum { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="st_description")]
		public string StDescription { get; set; }

	}
}
