using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class UserInfoService : BaseService<UserInfo>, IUserInfoService
	{
		private IUserInfoDal _userInfoDal { get; set; }
		public UserInfoService(IUserInfoDal userInfoDal)
		{
			_userInfoDal = userInfoDal;
			BaseDal = _userInfoDal;
		}

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">包含用户名，密码</param>
        /// <returns>成功返回对象，失败返回null</returns>
        public UserInfo Login(UserInfo user)
        {
            user.UserPwd = EncryptHelper.Get32MD5(user.UserPwd);
            user.UserPwd = EncryptHelper.Get32MD5(user.UserPwd);
            return _userInfoDal.Login(user);
        }
    }
}
