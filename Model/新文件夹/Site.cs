using SqlSugar;
using System;

namespace Model

{
	[SugarTable("tb_site")]
	public class Site
	{
		/// </summary>
		/// 站点ID
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="site_Id")]
		public int SiteId { get; set; }

		/// </summary>
		/// 站点序列号
		/// </summary>
		[SugarColumn(ColumnName ="site_SN")]
		public int SiteSN { get; set; }

		/// </summary>
		/// 站点类型
		/// </summary>
		[SugarColumn(ColumnName ="site_Type")]
		public int SiteType { get; set; }

		/// </summary>
		/// 站点IP地址
		/// </summary>
		[SugarColumn(ColumnName ="site_Ip")]
		public string SiteIP { get; set; }

		/// </summary>
		/// 站点端口
		/// </summary>
		[SugarColumn(ColumnName ="site_Port")]
		public int SitePort { get; set; }

		/// </summary>
		/// 站点运行状态
		/// </summary>
		[SugarColumn(ColumnName ="site_RunState")]
		public int SiteRunState { get; set; }

		/// </summary>
		/// 站点结构更新标记
		/// </summary>
		[SugarColumn(ColumnName ="site_UpdateTag")]
		public int SiteUpdateTag { get; set; }

		/// </summary>
		/// 站点固件版本号
		/// </summary>
		[SugarColumn(ColumnName ="site_Ver")]
		public string SiteVer { get; set; }

		/// </summary>
		/// 站点首次运行时间
		/// </summary>
		[SugarColumn(ColumnName ="site_FirstTimes")]
		public DateTime SiteFirstTimes { get; set; }

		/// </summary>
		/// 站点最后运行时间
		/// </summary>
		[SugarColumn(ColumnName ="site_FinallyTimes")]
		public DateTime SiteFinallyTimes { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="site_DateTime")]
		public DateTime SIteDateTime { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="site_StorageUseSize")]
		public int SiteStorageUseSize { get; set; }

		/// </summary>
		/// 站点备注信息
		/// </summary>
		[SugarColumn(ColumnName ="site_Remark")]
		public string SiteRemark { get; set; }

		/// </summary>
		/// 外键_区域ID
		/// </summary>
		[SugarColumn(ColumnName ="area_Id")]
		public int AreaId { get; set; }

	}
}
