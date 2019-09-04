using SqlSugar;

namespace Model

{
	[SugarTable("tb_config_bind")]
	public class ConfigBind
	{
		/// </summary>
		/// 绑定ID
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="bind_id")]
		public int bindId { get; set; }

		/// </summary>
		/// 区域ID
		/// </summary>
		[SugarColumn(ColumnName ="area_Id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 机器ID
		/// </summary>
		[SugarColumn(ColumnName ="mc_Id")]
		public int McId { get; set; }

		/// </summary>
		/// 测点ID
		/// </summary>
		[SugarColumn(ColumnName ="par_Id")]
		public int ParId { get; set; }

		/// </summary>
		/// 方向ID
		/// </summary>
		[SugarColumn(ColumnName ="dir_Id")]
		public int DirId { get; set; }

		/// </summary>
		/// 站点ID
		/// </summary>
		[SugarColumn(ColumnName ="site_Id")]
		public int SiteId { get; set; }

		/// </summary>
		/// 组板ID
		/// </summary>
		[SugarColumn(ColumnName ="group_id")]
		public int GroupId { get; set; }

		/// </summary>
		/// 通道ID
		/// </summary>
		[SugarColumn(ColumnName ="channel_Id")]
		public int ChannelId { get; set; }

		/// </summary>
		/// 传感器ID
		/// </summary>
		[SugarColumn(ColumnName ="sensorId")]
		public int SensorId { get; set; }

		/// </summary>
		/// 传感器类型
		/// </summary>
		[SugarColumn(ColumnName ="sensorType")]
		public int SensorType { get; set; }

		/// </summary>
		/// 是否激活 0不激活 1激活
		/// </summary>
		[SugarColumn(ColumnName ="cb_isActivate")]
		public int CbIsActivate { get; set; }

	}
}
