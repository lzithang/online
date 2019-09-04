using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace VS.IService
{
    public interface ICompanyService : IBaseService<Company>
    {
        /// <summary>
        /// ��ȡ��˾���������νṹ������
        /// </summary>
        /// <param name="cpId"></param>
        /// <returns></returns>
        ResultData GetTreeAll(int cpId);

    }
}
