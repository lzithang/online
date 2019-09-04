using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_motor")]
	public class DirverMotor
	{
		/// </summary>
		/// 电动机ID
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="dm_id")]
		public int DmId { get; set; }

		/// </summary>
		/// 描述
		/// </summary>
		[SugarColumn(ColumnName ="dm_name")]
		public string DmName { get; set; }

		/// </summary>
		/// 标识
		/// </summary>
		[SugarColumn(ColumnName ="dm_mark")]
		public string DmMark { get; set; }

		/// </summary>
		/// 电动机类型:1:交流；2：直流；
		/// </summary>
		[SugarColumn(ColumnName ="dm_type")]
		public int DmType { get; set; }

		/// </summary>
		/// 工频
		/// </summary>
		[SugarColumn(ColumnName ="dm_lf")]
		public float DmLf { get; set; }

		/// </summary>
		/// 级数
		/// </summary>
		[SugarColumn(ColumnName ="dm_poles")]
		public float DmPoles { get; set; }

		/// </summary>
		/// 转速
		/// </summary>
		[SugarColumn(ColumnName ="dm_speed")]
		public float DmSpeed { get; set; }

		/// </summary>
		/// 笼条数(同步电机无)
		/// </summary>
		[SugarColumn(ColumnName ="dm_bars")]
		public float DmBars { get; set; }

		/// </summary>
		/// 槽数
		/// </summary>
		[SugarColumn(ColumnName ="dm_slots")]
		public float DmSlots { get; set; }

		/// </summary>
		/// 整流
		/// </summary>
		[SugarColumn(ColumnName ="dm_scr")]
		public float DmScr { get; set; }

		/// </summary>
		/// 汇流条数
		/// </summary>
		[SugarColumn(ColumnName ="dm_comm_bar")]
		public float DmCommBar { get; set; }

		/// </summary>
		/// 电刷数
		/// </summary>
		[SugarColumn(ColumnName ="dm_brush")]
		public float DmBrush { get; set; }

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
		/// 测点Id集合
		/// </summary>
		[SugarColumn(ColumnName ="par_ids")]
		public string ParIds { get; set; }

	}
}
