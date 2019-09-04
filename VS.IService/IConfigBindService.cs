using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IService
{
	public interface IConfigBindService:IBaseService<ConfigBind>
	{
        /// <summary>
        /// ��ȡChannelStruct���Ͳ�����Ϣ
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        List<ConfigBindModel> GetChannelStructInfo(int sn);
    }
}
