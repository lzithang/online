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
        /// ��¼
        /// </summary>
        /// <param name="user">�����û���������</param>
        /// <returns>�ɹ����ض���ʧ�ܷ���null</returns>
        public UserInfo Login(UserInfo user)
        {
            user.UserPwd = EncryptHelper.Get32MD5(user.UserPwd);
            user.UserPwd = EncryptHelper.Get32MD5(user.UserPwd);
            return _userInfoDal.Login(user);
        }
    }
}
