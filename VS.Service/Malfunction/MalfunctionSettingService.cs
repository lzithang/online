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


	}
}
