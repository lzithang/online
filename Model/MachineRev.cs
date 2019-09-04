using SqlSugar;
using System;

namespace Model

{
	[SugarTable("tb_machine_rev")]
	public class MachineRev
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="mr_id")]
		public int MrId { get; set; }

		/// </summary>
		/// 转速
		/// </summary>
		[SugarColumn(ColumnName ="mr_rev")]
		public float MrRev { get; set; }

		/// </summary>
		/// 优先级别：1、输入转速；2、提取转速；3、测量转速；
		/// </summary>
		[SugarColumn(ColumnName ="mr_level")]
		public int MrLevel { get; set; }

		/// </summary>
		/// 区域ID
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 机器ID
		/// </summary>
		[SugarColumn(ColumnName ="mc_id")]
		public int McId { get; set; }

		/// </summary>
		/// 分析时间
		/// </summary>
		[SugarColumn(ColumnName ="mr_time")]
		public DateTime MrTime { get; set; }

	}
}
