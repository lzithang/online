using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface ICompanyDal:IBaseRepository<Company>
	{
        /// <summary>
        /// ��ȡ��˾���������νṹ������
        /// </summary>
        /// <param name="cpId"></param>
        /// <returns></returns>
        object GetTreeAll(int cpId);

    }
}
