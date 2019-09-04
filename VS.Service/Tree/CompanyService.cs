using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class CompanyService : BaseService<Company>, ICompanyService
	{
		private ICompanyDal _companyDal { get; set; }
		public CompanyService(ICompanyDal companyDal)
		{
			_companyDal = companyDal;
			BaseDal = _companyDal;
		}

        /// <summary>
        /// 获取树形结构
        /// </summary>
        /// <param name="cpId"></param>
        /// <returns></returns>
        public ResultData GetTreeAll(int cpId)
        {
            object obj =_companyDal.GetTreeAll(cpId);
            return new ResultData(obj);
        }
    }
}
