using SqlSugar;
using System;
using System.Collections.Generic;

namespace Model

{
	[SugarTable("tb_user_info")]
	public class UserInfo
	{
		/// </summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int UserId { get; set; }

		/// </summary>
		/// 用户名
		/// </summary>
		public string UserName { get; set; }

        /// </summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

  //      /// </summary>
		///// 用户创建人数限制
		///// </summary>
		//public int UserCount { get; set; }

  //      /// </summary>
  //      /// 用户凭证
  //      /// </summary>
  //      public string UserToken { get; set; }

		///// </summary>
		///// 父级Id
		///// </summary>
		//public int ParentId { get; set; }

		///// </summary>
		///// 工厂Ids
		///// </summary>
		//public string FtIds { get; set; }

		/// </summary>
		/// 公司Id
		/// </summary>
		public int CpId { get; set; }

		/// </summary>
		/// 邮箱
		/// </summary>
		public string UserEmail { get; set; }

		/// </summary>
		/// 用户手机号
		/// </summary>
		public string UserTel { get; set; }

		/// </summary>
		/// 备注
		/// </summary>
		public string UserRemark { get; set; }

		/// </summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }

        /// <summary>
        /// 公司信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Company CompanyInfo { get; set; }

        /// <summary>
        /// 工厂信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Factory> FactoryList { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        [SugarColumn(IsIgnore =true)]
        public List<Role> Roles { get; set; }

    }
}
