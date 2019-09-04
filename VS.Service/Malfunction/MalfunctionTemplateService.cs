using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class MalfunctionTemplateService : BaseService<MalfunctionTemplate>, IMalfunctionTemplateService
	{
		private IMalfunctionTemplateDal _malfunctionTemplateDal { get; set; }
		public MalfunctionTemplateService(IMalfunctionTemplateDal malfunctionTemplateDal)
		{
			_malfunctionTemplateDal = malfunctionTemplateDal;
			BaseDal = _malfunctionTemplateDal;
		}


	}
}
