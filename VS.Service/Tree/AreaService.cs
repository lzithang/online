using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class AreaService : BaseService<Area>, IAreaService
	{
		private IAreaDal _areaDal { get; set; }
		public AreaService(IAreaDal areaDal)
		{
			_areaDal = areaDal;
			BaseDal = _areaDal;
		}

        /// <summary>
        /// 获取树形结构
        /// </summary>
        /// <returns></returns>
        public ResultData GetTreeAll()
        {
            object obj = _areaDal.GetTreeAll();
            return new ResultData(obj);
        }

        /// <summary>
        /// 获取路径名称
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="mcId"></param>
        /// <param name="parId"></param>
        /// <returns></returns>
        public PathName GetPathName(int areaId, int mcId, int parId)
        {
            return _areaDal.GetPathName(areaId, mcId, parId);
        }
    }
}
