using SqlSugar;

namespace Model

{
	[SugarTable("tb_sensor_temp")]
	public class SensorTemp
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="temp_Id")]
		public int TempId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="temp_name")]
		public string TempName { get; set; }

		/// </summary>
		/// ICP电源开关
		/// </summary>
		[SugarColumn(ColumnName ="temp_ICP")]
		public int TempICP { get; set; }

		/// </summary>
		/// 类型
		/// </summary>
		[SugarColumn(ColumnName ="temp_type")]
		public int TempType { get; set; }

		/// </summary>
		/// 温度单位
		/// </summary>
		[SugarColumn(ColumnName ="temp_unit")]
		public int TempUnit { get; set; }

		/// </summary>
		/// 灵敏度
		/// </summary>
		[SugarColumn(ColumnName ="temp_sensibility")]
		public double TempSensibility { get; set; }

		/// </summary>
		/// 稳定时间
		/// </summary>
		[SugarColumn(ColumnName ="temp_settlingTime")]
		public int TempSettlingTime { get; set; }

		/// </summary>
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName ="temp_remark")]
		public string TempRemark { get; set; }

	}
}
