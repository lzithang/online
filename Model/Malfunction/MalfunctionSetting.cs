using SqlSugar;

namespace Model

{
	[SugarTable("tb_malfunction_setting")]
	public class MalfunctionSetting
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="ms_id")]
		public int MsId { get; set; }

		/// </summary>
		/// 区域id
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 机器Id
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 测点Id
		/// </summary>
		[SugarColumn(ColumnName ="par_id")]
		public int ParId { get; set; }

		/// </summary>
		/// 方向Id
		/// </summary>
		[SugarColumn(ColumnName ="dir_id")]
		public int DirId { get; set; }

		/// </summary>
		/// 所属模板Id
		/// </summary>
		[SugarColumn(ColumnName ="mt_id")]
		public int MtId { get; set; }

		/// </summary>
		/// 故障Id
		/// </summary>
		[SugarColumn(ColumnName ="type_id")]
		public int TypeId { get; set; }

		/// </summary>
		/// 诊断名称
		/// </summary>
		[SugarColumn(ColumnName ="type_name")]
		public string TypeName { get; set; }

		/// </summary>
		/// 数据源id
		/// </summary>
		[SugarColumn(ColumnName ="msr_id")]
		public int MsrId { get; set; }

		/// </summary>
		/// 数据源名称
		/// </summary>
		[SugarColumn(ColumnName ="data_name")]
		public string DataName { get; set; }

		/// </summary>
		/// 元件名称
		/// </summary>
		[SugarColumn(ColumnName ="unit_name")]
		public string UnitName { get; set; }

		/// </summary>
		/// 元件类型
		/// </summary>
		[SugarColumn(ColumnName ="unit_type")]
		public int UnitType { get; set; }

		/// </summary>
		/// 元件Id
		/// </summary>
		[SugarColumn(ColumnName ="unit_id")]
		public int UnitId { get; set; }

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
		/// 通用
		/// </summary>
		[SugarColumn(ColumnName ="common_formula")]
		public string CommonFormula { get; set; }

		/// </summary>
		/// 工频
		/// </summary>
		[SugarColumn(ColumnName ="remove_frequency")]
		public string RemoveFrequency { get; set; }

		/// </summary>
		/// 诊断方法
		/// </summary>
		[SugarColumn(ColumnName ="mm_id")]
		public int MmId { get; set; }

		/// </summary>
		/// 1百分比，2频段 3谱线数
		/// </summary>
		[SugarColumn(ColumnName ="find_type")]
		public int FindType { get; set; }

		/// </summary>
		/// 峰值范围百分比
		/// </summary>
		[SugarColumn(ColumnName ="ms_range")]
		public float MsRange { get; set; }

		/// </summary>
		/// 谱线数
		/// </summary>
		[SugarColumn(ColumnName ="ms_line")]
		public float MsLine { get; set; }

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
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName ="remark")]
		public string Remark { get; set; }

	}
}
