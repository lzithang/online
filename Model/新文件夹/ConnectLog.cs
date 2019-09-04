using SqlSugar;
using System;

namespace Model

{
	[SugarTable("tb_connect_log")]
	public class ConnectLog
	{
		/// </summary>
		/// 主键id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="cl_id")]
		public int ClId { get; set; }

		/// </summary>
		/// 连接时间
		/// </summary>
		[SugarColumn(ColumnName ="cl_time")]
		public DateTime ClTime { get; set; }

		/// </summary>
		/// 站点编号
		/// </summary>
		[SugarColumn(ColumnName ="site_sn")]
		public int SiteSn { get; set; }

	}
}
