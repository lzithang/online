using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Model;
using VS.Common;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
    /// <summary>
    /// 总值服务层
    /// </summary>
	public class DataOaService : BaseService<DataOa>, IDataOaService
	{
        private IDataOaDal _dataOaDal;
        private IMapper _mapper;
		public DataOaService(IDataOaDal dataOaDal,IMapper mapper)
		{
			_dataOaDal = dataOaDal;
			BaseDal = _dataOaDal;
            _mapper = mapper;
		}

        /// <summary>
        /// 获取方向下最新 count条数据
        /// </summary>
        /// <param name="ids">方向</param>
        /// <param name="count">数量</param>
        /// <returns>结果集</returns>
        public ResultData GetDataOaByDirId(IdsModel ids, int count)
        {
            List<DataOa> list = _dataOaDal.GetDataOaByDirId(ids, count);
            List<DataOaModel> domList = new List<DataOaModel>();
            foreach (DataOa oa in list)
            {
                domList.Add(_mapper.Map<DataOaModel>(oa));
            }
            return new ResultData(domList);
        }

        /// <summary>
        /// 获取所有方向下最新一笔数据
        /// </summary>
        /// <returns>结果集</returns>
        public ResultData GetDataOaNew()
        {
            List<DataOa> list = _dataOaDal.GetDataOaNew();
            List<DataOaModel> domList = new List<DataOaModel>();
            foreach (DataOa oa in list)
            {
                domList.Add(_mapper.Map<DataOaModel>(oa));
            }
            return new ResultData(domList);
        }

        /// <summary>
        /// 获取机器下所有方向最新一笔数据
        /// </summary>
        /// <returns>结果集</returns>
        public ResultData GetDataOaNewByMcId(int areaId, int mcId)
        {
            List<DataOa> list = _dataOaDal.GetDataOaNewByMcId(areaId,mcId);
            List<DataOaModel> domList = new List<DataOaModel>();
            foreach (DataOa oa in list)
            {
                domList.Add(_mapper.Map<DataOaModel>(oa));
            }
            return new ResultData(domList);
        }

        /// <summary>
        /// 获取方向下所有总值，每一项是个数组
        /// </summary>
        /// <param name="ids">方向</param>
        public ResultData GetDataOaByDirId(IdsModel ids)
        {
            object obj = _dataOaDal.GetDataOaByDirId(ids);
            return new ResultData( obj);
        }

        /// <summary>
        /// 查询方向下的时间数据列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultData GetDataOaTimeList(IdsModel ids)
        {
            List<TimeModel> list = _dataOaDal.GetDataOaTimeList(ids);
            return new ResultData(list);
        }
    }
}
