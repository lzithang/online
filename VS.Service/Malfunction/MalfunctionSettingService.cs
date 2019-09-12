using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MalfunctionSettingService : BaseService<MalfunctionSetting>, IMalfunctionSettingService
	{
		private IMalfunctionSettingDal _malfunctionSettingDal { get; set; }
		public MalfunctionSettingService(IMalfunctionSettingDal malfunctionSettingDal)
		{
			_malfunctionSettingDal = malfunctionSettingDal;
			BaseDal = _malfunctionSettingDal;
		}

        /// <summary>
        /// ��������Id ��ȡ���������Ϊ��ʽ
        /// </summary>
        /// <param name="msrId">����ԴId</param>
        /// <returns></returns>
        public List<MalfunctionSetting> GetMalfunctionSettingListByMsrId(int msrId)
        {
            return _malfunctionSettingDal.GetMalfunctionSettingListByMsrId(msrId);
        }

    }
}
