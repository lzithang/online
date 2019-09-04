using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IUserInfoDal:IBaseRepository<UserInfo>
	{
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">包含用户名，密码</param>
        /// <returns>成功返回对象，失败返回null</returns>
        UserInfo Login(UserInfo user);

    }
}
