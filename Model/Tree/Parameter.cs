using SqlSugar;

namespace Model

{
	[SugarTable("tb_parameter")]
	public class Parameter
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_id")]
		public int ParId { get; set; }

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
		[SugarColumn(ColumnName ="par_bearing")]
		public string ParBearing { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_multiple")]
		public double ParMultiple { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_t")]
		public int ParT { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_img_id")]
		public int ParImgId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_description")]
		public string ParDescription { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_state")]
		public int ParState { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_alarm1")]
		public double ParAlarm1 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="par_alarm2")]
		public double ParAlarm2 { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="unit_id")]
		public int UnitId { get; set; }

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
		[SugarColumn(ColumnName ="par_tacho")]
		public int ParTacho { get; set; }

		/// </summary>
		/// 0,1代表皮带风机 2直接驱动风机 3直接驱动泵,4一主齿轮驱动多个从动齿轮,5多级减速机,6行星齿轮箱
		/// </summary>
		[SugarColumn(ColumnName ="dirver_type")]
		public int DirverType { get; set; }

	}
}
