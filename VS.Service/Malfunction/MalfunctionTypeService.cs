using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MalfunctionTypeService : BaseService<MalfunctionType>, IMalfunctionTypeService
	{
		private IMalfunctionTypeDal _malfunctionTypeDal { get; set; }
		public MalfunctionTypeService(IMalfunctionTypeDal malfunctionTypeDal)
		{
			_malfunctionTypeDal = malfunctionTypeDal;
			BaseDal = _malfunctionTypeDal;
		}


	}
}
