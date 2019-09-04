using SqlSugar;
using System;

namespace Model

{
	[SugarTable("tb_site_manual")]
	public class SiteManual
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="smc_Id")]
		public int SmcId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="site_sn")]
		public int SiteSn { get; set; }

		/// </summary>
		/// 参数设置
		/// </summary>
		[SugarColumn(ColumnName ="smc_parameter")]
		public string SmcParameter { get; set; }

		/// </summary>
		/// 最小频率
		/// </summary>
		[SugarColumn(ColumnName ="smc_rateMin")]
		public int SmcRateMin { get; set; }

		/// </summary>
		/// 最大频率
		/// </summary>
		[SugarColumn(ColumnName ="smc_rateMax")]
		public int SmcRateMax { get; set; }

		/// </summary>
		/// 谱线数
		/// </summary>
		[SugarColumn(ColumnName ="smc_line")]
		public int SmcLine { get; set; }

		/// </summary>
		/// 1：频谱 2：波形采集 3：频谱+波形采集
		/// </summary>
		[SugarColumn(ColumnName ="smc_FFT_WT")]
		public int SmcFFTWT { get; set; }

		/// </summary>
		/// 平均类型
		/// </summary>
		[SugarColumn(ColumnName ="smc_avgType")]
		public int SmcAvgType { get; set; }

		/// </summary>
		/// 平均数
		/// </summary>
		[SugarColumn(ColumnName ="smc_avgNum")]
		public int SmcAvgNum { get; set; }

		/// </summary>
		/// 窗函数
		/// </summary>
		[SugarColumn(ColumnName ="smc_windowFun")]
		public int SmcWindowFun { get; set; }

		/// </summary>
		/// 叠加百分比
		/// </summary>
		[SugarColumn(ColumnName ="smc_percent")]
		public int SmcPercent { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="start_time")]
		public DateTime StartTime { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="end_time")]
		public DateTime endTime { get; set; }

		/// </summary>
		/// 0未完成 1完成
		/// </summary>
		[SugarColumn(ColumnName ="isComplete")]
		public int IsComplete { get; set; }

		/// </summary>
		/// 1取消
		/// </summary>
		[SugarColumn(ColumnName ="isCancel")]
		public int IsCancel { get; set; }

		/// </summary>
		/// 手动采集通道
		/// </summary>
		[SugarColumn(ColumnName ="channels")]
		public string Channels { get; set; }

	}
}
