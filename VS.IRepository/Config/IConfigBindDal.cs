using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
    public interface IConfigBindDal : IBaseRepository<ConfigBind>
    {
        /// <summary>
        /// 获取ChannelStruct类型部分信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        List<ConfigBindModel> GetChannelStructInfo(int sn);

    }
}
