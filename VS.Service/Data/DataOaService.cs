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
    /// ��ֵ�����
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
        /// ��ȡ���������� count������
        /// </summary>
        /// <param name="ids">����</param>
        /// <param name="count">����</param>
        /// <returns>�����</returns>
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
        /// ��ȡ���з���������һ������
        /// </summary>
        /// <returns>�����</returns>
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
        /// ��ȡ���������з�������һ������
        /// </summary>
        /// <returns>�����</returns>
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
        /// ��ȡ������������ֵ��ÿһ���Ǹ�����
        /// </summary>
        /// <param name="ids">����</param>
        public ResultData GetDataOaByDirId(IdsModel ids)
        {
            object obj = _dataOaDal.GetDataOaByDirId(ids);
            return new ResultData( obj);
        }

        /// <summary>
        /// ��ѯ�����µ�ʱ�������б�
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
