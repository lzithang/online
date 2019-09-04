using SqlSugar;

namespace Model

{
	[SugarTable("tb_company")]
	public class Company
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_id",IsIdentity = true,IsPrimaryKey = true)]
		public int CpId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_name")]
		public string CpName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_img_id")]
		public int CpImgId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_database")]
		public string CpDatabase { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_address")]
		public string CpAddress { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_contactInfo")]
		public string CpContactInfo { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_description")]
		public string CpDescription { get; set; }

	}
}
