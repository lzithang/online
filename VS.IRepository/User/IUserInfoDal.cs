using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IUserInfoDal:IBaseRepository<UserInfo>
	{
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="user">�����û���������</param>
        /// <returns>�ɹ����ض���ʧ�ܷ���null</returns>
        UserInfo Login(UserInfo user);

    }
}
