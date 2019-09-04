using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
    public class UserInfoDal : BaseRepository<UserInfo>, IUserInfoDal
    {
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="user">�����û���������</param>
        /// <returns>�ɹ����ض���ʧ�ܷ���null</returns>
        public UserInfo Login(UserInfo user)
        {
            user = Db.Queryable<UserInfo>().Where(u => u.UserName == user.UserName && u.UserPwd == user.UserPwd).First();
            if (user != null)
            {
                user.CompanyInfo = Db.Ado.SqlQuerySingle<Company>("SELECT * FROM tb_company WHERE cp_id =@CpId", new { CpId = user.CpId });
                user.FactoryList = Db.Ado.SqlQuery<Factory>("SELECT * FROM tb_factory WHERE cp_id =@CpId", new { CpId = user.CpId });
                user.Roles = Db.Ado.SqlQuery<Role>("SELECT * FROM tb_Role WHERE role_id in (SELECT role_id FROM tb_user_role where user_id = @UserId)", new { UserId = user.UserId });
            }
            return user;
        }
    }
}
