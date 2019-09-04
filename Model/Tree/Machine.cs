using SqlSugar;

namespace Model

{
	[SugarTable("tb_machine")]
	public class Machine
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="instrument_type")]
		public string InstrumentType { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_code")]
		public string McCode { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_name")]
		public string McName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_grade")]
		public string McGrade { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_img_id")]
		public int McImgId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_description")]
		public string McDescription { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_bpdm")]
		public string McBpdm { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_framework")]
		public string McFramework { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="mc_rev")]
		public double McRev { get; set; }

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
		[SugarColumn(ColumnName ="parIdMark")]
		public int ParIdMark { get; set; }

	}
}
