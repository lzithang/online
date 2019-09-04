using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MachineRevService : BaseService<MachineRev>, IMachineRevService
	{
		private IMachineRevDal _machineRevDal { get; set; }
		public MachineRevService(IMachineRevDal machineRevDal)
		{
			_machineRevDal = machineRevDal;
			BaseDal = _machineRevDal;
		}


	}
}
