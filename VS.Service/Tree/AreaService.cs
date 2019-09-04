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
        /// ��ȡ���νṹ
        /// </summary>
        /// <returns></returns>
        public ResultData GetTreeAll()
        {
            object obj = _areaDal.GetTreeAll();
            return new ResultData(obj);
        }

        /// <summary>
        /// ��ȡ·������
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
