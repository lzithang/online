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
        /// 根据数据Id 获取诊断项，诊断项为公式
        /// </summary>
        /// <param name="msrId">数据源Id</param>
        /// <returns></returns>
        public List<MalfunctionSetting> GetMalfunctionSettingListByMsrId(int msrId)
        {
            return _malfunctionSettingDal.GetMalfunctionSettingListByMsrId(msrId);
        }

    }
}
