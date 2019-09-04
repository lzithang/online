using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface ICompanyDal:IBaseRepository<Company>
	{
        /// <summary>
        /// 获取公司下所有树形结构体数据
        /// </summary>
        /// <param name="cpId"></param>
        /// <returns></returns>
        object GetTreeAll(int cpId);

    }
}
