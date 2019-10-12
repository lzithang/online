using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MachineStopService : BaseService<MachineStop>, IMachineStopService
    {
		private IMachineStopDal _machineStopDal { get; set; }
		public MachineStopService(IMachineStopDal machineStopDal)
		{
            _machineStopDal = machineStopDal;
			BaseDal = _machineStopDal;
		}


	}
}
