using SqlSugar;

namespace Model

{
	[SugarTable("tb_user_role")]
	public class UserRole
	{
		/// </summary>
		/// 主键
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true,ColumnName ="ur_id")]
		public int UrId { get; set; }

		/// </summary>
		/// 用户Id
		/// </summary>
		[SugarColumn(ColumnName ="user_id")]
		public int UserId { get; set; }

		/// </summary>
		/// 角色Id
		/// </summary>
		[SugarColumn(ColumnName ="role_id")]
		public int RoleId { get; set; }

	}
}
