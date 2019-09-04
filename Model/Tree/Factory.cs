using SqlSugar;

namespace Model

{
	[SugarTable("tb_factory")]
	public class Factory
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="ft_id")]
		public int FtId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="ft_name")]
		public string FtName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="ft_address")]
		public string FtAddress { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="ft_contactInfo")]
		public string FtContactInfo { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="ft_description")]
		public string FtDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "ft_database")]
        public string FtDatabase { get; set; }
        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName ="img_id")]
		public int ImgId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="cp_id")]
		public int CpId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="areaIdMark")]
		public int AreaIdMark { get; set; }

	}
}
