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
    public class DataTwService : BaseService<DataTw>, IDataTwService
    {
        private IDataTwDal _dataTwDal;
        private IMapper _mapper;

        public DataTwService(IDataTwDal dataTwDal, IMapper mapper)
        {
            _mapper = mapper;
            _dataTwDal = dataTwDal;
            BaseDal = _dataTwDal;
        }

        /// <summary>
        /// 获取方向下的时间列表（包含工况）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ids">方向</param>
        /// <returns>时间列表(含对应工况)</returns>
        public ResultData GetDataTwTimeList(string parameter, IdsModel ids)
        {

            object obj = _dataTwDal.GetDataTwTimeList(GetTableName(parameter), ids);
            return new ResultData(obj);
        }

        /// <summary>
        /// 获取方向下某时间波形
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ids">方向</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public ResultData GetDataTwByDirId(string parameter, IdsModel ids, TimeModel time)
        {
            List<DataTw> list = _dataTwDal.GetDataTwByDirId(GetTableName(parameter), ids, time);
            List<DataTwModel> dtmList = new List<DataTwModel>();
            foreach (DataTw item in list)
            {
                dtmList.Add(_mapper.Map<DataTwModel>(item));
            }
            return new ResultData(dtmList);
        }

        /// <summary>
        /// 插入波形
        /// </summary>
        /// <param name="dataTw"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool InsertDataTw(DataTw dataTw, string tableName)
        {
            return _dataTwDal.InsertDataTw(dataTw, tableName);
        }

        private string GetTableName(string parameter)
        {
            return $"tb_data_tw_{parameter}";
        }

    }
}
