using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MachineService : BaseService<Machine>, IMachineService
	{
		private IMachineDal _machineDal { get; set; }
		public MachineService(IMachineDal machineDal)
		{
			_machineDal = machineDal;
			BaseDal = _machineDal;
		}


	}
}
