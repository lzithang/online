using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class RoleService : BaseService<Role>, IRoleService
	{
		private IRoleDal _roleDal { get; set; }
		public RoleService(IRoleDal roleDal)
		{
			_roleDal = roleDal;
			BaseDal = _roleDal;
		}


	}
}
