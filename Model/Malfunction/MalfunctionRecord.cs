using SqlSugar;
using System;

namespace Model

{
	[SugarTable("tb_malfunction_record")]
	public class MalfunctionRecord
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="mr_id")]
		public int MrId { get; set; }

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
		/// 报警值
		/// </summary>
		[SugarColumn(ColumnName ="mr_value")]
		public float MrValue { get; set; }

		/// </summary>
		/// 报警级别
		/// </summary>
		[SugarColumn(ColumnName ="mr_level")]
		public int MrLevel { get; set; }

		/// </summary>
		/// 报警时间
		/// </summary>
		[SugarColumn(ColumnName ="begin_time")]
		public DateTime BeginTime { get; set; }

		/// </summary>
		/// 处理时间
		/// </summary>
		[SugarColumn(ColumnName ="end_time")]
		public DateTime EndTime { get; set; }

		/// </summary>
		/// 状态
		/// </summary>
		[SugarColumn(ColumnName ="mr_status")]
		public int MrStatus { get; set; }

	}
}
