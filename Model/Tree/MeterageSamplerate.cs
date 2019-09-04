using SqlSugar;

namespace Model

{
	[SugarTable("tb_meterage_samplerate")]
	public class MeterageSamplerate
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="msr_id")]
		public int MsrId { get; set; }

		/// </summary>
		/// 频率名称
		/// </summary>
		[SugarColumn(ColumnName ="msr_name")]
		public string MsrName { get; set; }

		/// </summary>
		/// 参数设置
		/// </summary>
		[SugarColumn(ColumnName ="msr_parameter")]
		public string MsrParameter { get; set; }

		/// </summary>
		/// 最小频率
		/// </summary>
		[SugarColumn(ColumnName ="msr_rateMin")]
		public int MsrRateMin { get; set; }

		/// </summary>
		/// 最大频率
		/// </summary>
		[SugarColumn(ColumnName ="msr_rateMax")]
		public int MsrRateMax { get; set; }

		/// </summary>
		/// 谱线数
		/// </summary>
		[SugarColumn(ColumnName ="msr_line")]
		public int MsrLine { get; set; }

		/// </summary>
		/// 1：频谱 2：波形采集 3：频谱+波形采集
		/// </summary>
		[SugarColumn(ColumnName ="msr_FFT_WT")]
		public int MsrFFTWT { get; set; }

		/// </summary>
		/// 平均类型
		/// </summary>
		[SugarColumn(ColumnName ="msr_avgType")]
		public int MsrAvgType { get; set; }

		/// </summary>
		/// 平均数
		/// </summary>
		[SugarColumn(ColumnName ="msr_avgNum")]
		public int MsrAvgNum { get; set; }

		/// </summary>
		/// 窗函数
		/// </summary>
		[SugarColumn(ColumnName ="msr_windowFun")]
		public int MsrWindowFun { get; set; }

		/// </summary>
		/// 叠加百分比
		/// </summary>
		[SugarColumn(ColumnName ="msr_percent")]
		public int MsrPercent { get; set; }

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
		[SugarColumn(ColumnName ="par_id")]
		public int ParId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="dir_id")]
		public int DirId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="group_id")]
		public int GroupId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="msr_isLongData")]
		public int MsrIsLongData { get; set; }

		/// </summary>
		/// 测量参数库Id
		/// </summary>
		[SugarColumn(ColumnName ="ml_id")]
		public int MlId { get; set; }

		/// </summary>
		/// 0不采集 1采集
		/// </summary>
		[SugarColumn(ColumnName ="isSamplerate")]
		public int IsSamplerate { get; set; }

	}
}
