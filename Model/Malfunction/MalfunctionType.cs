using SqlSugar;

namespace Model

{
	[SugarTable("tb_malfunction_type")]
	public class MalfunctionType
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="type_id")]
		public int TypeId { get; set; }

		/// </summary>
		/// 故障名称
		/// </summary>
		[SugarColumn(ColumnName ="type_name")]
		public string TypeName { get; set; }

		/// </summary>
		/// 谱线数
		/// </summary>
		[SugarColumn(ColumnName ="type_line")]
		public float TypeLine { get; set; }

		/// </summary>
		/// 峰值查找范围
		/// </summary>
		[SugarColumn(ColumnName ="type_range")]
		public float TypeRange { get; set; }

		/// </summary>
		/// 中心频率
		/// </summary>
		[SugarColumn(ColumnName ="center_frequency")]
		public string CenterFrequency { get; set; }

		/// </summary>
		/// 边带
		/// </summary>
		[SugarColumn(ColumnName ="sideband_frequency")]
		public string SidebandFrequency { get; set; }

		/// </summary>
		/// 消除频率
		/// </summary>
		[SugarColumn(ColumnName ="remove_frequency")]
		public string RemoveFrequency { get; set; }

		/// </summary>
		/// 通用公式
		/// </summary>
		[SugarColumn(ColumnName ="common_formula")]
		public string CommonFormula { get; set; }

		/// </summary>
		/// 计算方式
		/// </summary>
		[SugarColumn(ColumnName ="mm_id")]
		public int MmId { get; set; }

		/// </summary>
		/// 模板Id
		/// </summary>
		[SugarColumn(ColumnName ="mt_id")]
		public int MtId { get; set; }

		/// </summary>
		/// 峰值查找类型 1百分比，2频带，3谱线数
		/// </summary>
		[SugarColumn(ColumnName ="find_type")]
		public int FindType { get; set; }

		/// </summary>
		/// 故障描述
		/// </summary>
		[SugarColumn(ColumnName ="remark")]
		public string Remark { get; set; }

		/// </summary>
		/// 预警
		/// </summary>
		[SugarColumn(ColumnName ="easy_warning")]
		public float EasyWarning { get; set; }

		/// </summary>
		/// 警告
		/// </summary>
		[SugarColumn(ColumnName ="warning")]
		public float Warning { get; set; }

		/// </summary>
		/// 危险
		/// </summary>
		[SugarColumn(ColumnName ="danger")]
		public float Danger { get; set; }

		/// </summary>
		/// 报警线计算方法 1 百分比 2绝对值
		/// </summary>
		[SugarColumn(ColumnName ="alarm_type")]
		public int AlarmType { get; set; }

	}
}
