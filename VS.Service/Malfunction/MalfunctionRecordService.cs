using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MalfunctionRecordService : BaseService<MalfunctionRecord>, IMalfunctionRecordService
	{
		private IMalfunctionRecordDal _malfunctionRecordDal { get; set; }
		public MalfunctionRecordService(IMalfunctionRecordDal malfunctionRecordDal)
		{
			_malfunctionRecordDal = malfunctionRecordDal;
			BaseDal = _malfunctionRecordDal;
		}


	}
}
