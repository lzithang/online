using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace VS.IService
{
    public interface ICompanyService : IBaseService<Company>
    {
        /// <summary>
        /// 获取公司下所有树形结构体数据
        /// </summary>
        /// <param name="cpId"></param>
        /// <returns></returns>
        ResultData GetTreeAll(int cpId);

    }
}
