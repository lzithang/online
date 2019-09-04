using SqlSugar;
using System;

namespace Model

{
	[SugarTable("tb_data_tw")]
	public class DataTw
	{
		/// </summary>
		/// 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="id")]
		public int Id { get; set; }

		/// </summary>
		/// 数据
		/// </summary>
		[SugarColumn(ColumnName ="data")]
		public byte[] Data { get; set; }

		/// </summary>
		/// 上传时间
		/// </summary>
		[SugarColumn(ColumnName ="time")]
		public DateTime Time { get; set; }

		/// </summary>
		/// 区域Id
		/// </summary>
		[SugarColumn(ColumnName ="area_id")]
		public int AreaId { get; set; }

		/// </summary>
		/// 机器id
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
		/// 数据状态
		/// </summary>
		[SugarColumn(ColumnName ="data_state")]
		public int DataState { get; set; }

		/// </summary>
		/// 基线
		/// </summary>
		[SugarColumn(ColumnName ="data_baseLine")]
		public int DataBaseLine { get; set; }

		/// </summary>
		/// 谱线数
		/// </summary>
		[SugarColumn(ColumnName ="data_lines")]
		public int DataLines { get; set; }

		/// </summary>
		/// 数据类型：1、全通；2、高通；3、包络谱
		/// </summary>
		[SugarColumn(ColumnName ="data_type")]
		public int DataType { get; set; }

		/// </summary>
		/// 采样点数
		/// </summary>
		[SugarColumn(ColumnName ="data_points")]
		public int DataPoints { get; set; }

		/// </summary>
		/// 采样频率
		/// </summary>
		[SugarColumn(ColumnName ="data_hz")]
		public int DataHz { get; set; }

		/// </summary>
		/// 0波形数据，1频谱数据
		/// </summary>
		[SugarColumn(ColumnName ="data_isFFT")]
		public int DataIsFFT { get; set; }

		/// </summary>
		/// 1长时间波形，0非
		/// </summary>
		[SugarColumn(ColumnName ="data_isLongData")]
		public int DataIsLongData { get; set; }

		/// </summary>
		/// 数据格式：1、二进制；2、十进制数
		/// </summary>
		[SugarColumn(ColumnName ="data_format")]
		public int DataFormat { get; set; }

		/// </summary>
		/// 工况
		/// </summary>
		[SugarColumn(ColumnName ="data_work_status")]
		public int DataWorkStatus { get; set; }

	}
}
