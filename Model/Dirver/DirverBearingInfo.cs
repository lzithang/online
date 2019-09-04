using SqlSugar;

namespace Model

{
	[SugarTable("tb_dirver_bearing_info")]
	public class DirverBearingInfo
	{
        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Id")]
        public int Id { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "Manufacturer")]
        public string Manufacturer { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "Type")]
        public string Type { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "NumberOfRollers")]
        public int NumberOfRollers { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "Retainer")]
        public float Retainer { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "Roller")]
        public float Roller { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "OuterRing")]
        public float OuterRing { get; set; }

        /// </summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "InsideRing")]
        public float InsideRing { get; set; }

        /// </summary>
		/// 
		/// </summary>
		[SugarColumn(ColumnName = "Remarks")]
        public string Remarks { get; set; }

    }
}
