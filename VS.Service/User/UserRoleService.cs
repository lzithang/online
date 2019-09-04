using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class UserRoleService : BaseService<UserRole>, IUserRoleService
	{
		private IUserRoleDal _userRoleDal { get; set; }
		public UserRoleService(IUserRoleDal userRoleDal)
		{
			_userRoleDal = userRoleDal;
			BaseDal = _userRoleDal;
		}


	}
}
