using SqlSugar;

namespace Model

{
	[SugarTable("tb_role")]
	public class Role
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="role_id")]
		public int RoleId { get; set; }

		/// </summary>
		/// 角色名称
		/// </summary>
		[SugarColumn(ColumnName ="role_name")]
		public string RoleName { get; set; }

		/// </summary>
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName ="role_remark")]
		public string RoleRemark { get; set; }

	}
}
