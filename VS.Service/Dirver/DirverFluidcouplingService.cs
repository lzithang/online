using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class DirverFluidcouplingService : BaseService<DirverFluidcoupling>, IDirverFluidcouplingService
	{
		private IDirverFluidcouplingDal _dirverFluidcouplingDal { get; set; }
		public DirverFluidcouplingService(IDirverFluidcouplingDal dirverFluidcouplingDal)
		{
			_dirverFluidcouplingDal = dirverFluidcouplingDal;
			BaseDal = _dirverFluidcouplingDal;
		}


	}
}
