using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IService
{
	public interface IUserInfoService:IBaseService<UserInfo>
	{
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="user">�����û���������</param>
        /// <returns>�ɹ����ض���ʧ�ܷ���null</returns>
        UserInfo Login(UserInfo user);

    }
}
