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
        /// ��ȡ�����µ�ʱ���б�����������
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="ids">����</param>
        /// <returns>ʱ���б�(����Ӧ����)</returns>
        public ResultData GetDataTwTimeList(string parameter, IdsModel ids)
        {

            object obj = _dataTwDal.GetDataTwTimeList(GetTableName(parameter), ids);
            return new ResultData(obj);
        }

        /// <summary>
        /// ��ȡ������ĳʱ�䲨��
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="ids">����</param>
        /// <param name="time">ʱ��</param>
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
        /// ���벨��
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
