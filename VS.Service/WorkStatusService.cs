using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class WorkStatusService : BaseService<WorkStatus>, IWorkStatusService
	{
		private IWorkStatusDal _workStatusDal { get; set; }
		public WorkStatusService(IWorkStatusDal workStatusDal)
		{
			_workStatusDal = workStatusDal;
			BaseDal = _workStatusDal;
		}

        /// <summary>
        /// ����ͨ��Id��ȡ��Ӧ�Ĺ���״̬
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public List<WorkStatus> GetWorkStatusByDirId(int areaId, int channelId)
        {
            return _workStatusDal.GetWorkStatusByDirId(areaId, channelId);
        }
    }
}
