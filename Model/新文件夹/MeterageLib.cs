using SqlSugar;

namespace Model

{
	[SugarTable("tb_meterage_lib")]
	public class MeterageLib
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="ml_id")]
		public int MlId { get; set; }

		/// </summary>
		/// 频率名称
		/// </summary>
		[SugarColumn(ColumnName ="ml_name")]
		public string MlName { get; set; }

		/// </summary>
		/// 参数设置
		/// </summary>
		[SugarColumn(ColumnName ="ml_parameter")]
		public string mlParameter { get; set; }

		/// </summary>
		/// 最小频率
		/// </summary>
		[SugarColumn(ColumnName ="ml_rateMin")]
		public int MlRateMin { get; set; }

		/// </summary>
		/// 最大频率
		/// </summary>
		[SugarColumn(ColumnName ="ml_rateMax")]
		public int MlRateMax { get; set; }

		/// </summary>
		/// 谱线数
		/// </summary>
		[SugarColumn(ColumnName ="ml_line")]
		public int mlLine { get; set; }

		/// </summary>
		/// 1：频谱 2：波形采集 3：频谱+波形采集
		/// </summary>
		[SugarColumn(ColumnName ="ml_FFT_WT")]
		public int MlFFTWT { get; set; }

		/// </summary>
		/// 平均类型
		/// </summary>
		[SugarColumn(ColumnName ="ml_avgType")]
		public int MlAvgType { get; set; }

		/// </summary>
		/// 平均数
		/// </summary>
		[SugarColumn(ColumnName ="ml_avgNum")]
		public int mlAvgNum { get; set; }

		/// </summary>
		/// 窗函数
		/// </summary>
		[SugarColumn(ColumnName ="ml_windowFun")]
		public int MlWindowFun { get; set; }

		/// </summary>
		/// 叠加百分比
		/// </summary>
		[SugarColumn(ColumnName ="ml_percent")]
		public int MlPercent { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="ml_isLongData")]
		public int MlIsLongData { get; set; }

	}
}
