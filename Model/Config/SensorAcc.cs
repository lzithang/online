using SqlSugar;

namespace Model

{
	[SugarTable("tb_sensor_acc")]
	public class SensorAcc
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="acc_Id")]
		public int AccId { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_name")]
		public string AccName { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_ICP")]
		public int AccICP { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_sensibility")]
		public double AccSensibility { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_SettlingTime")]
		public int AccSettlingTime { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_isBias")]
		public int AccIsBias { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_biasLow")]
		public double AccBiasLow { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_biasHigh")]
		public double AccBiasHigh { get; set; }

		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName ="acc_remark")]
		public string AccRemark { get; set; }

	}
}
