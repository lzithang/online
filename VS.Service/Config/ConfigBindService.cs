using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
	public class ConfigBindService : BaseService<ConfigBind>, IConfigBindService
	{
		private IConfigBindDal _configBindDal { get; set; }
		public ConfigBindService(IConfigBindDal configBindDal)
		{
			_configBindDal = configBindDal;
			BaseDal = _configBindDal;
		}

        /// <summary>
        /// 获取ChannelStruct类型部分信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public List<ConfigBindModel> GetChannelStructInfo(int sn)
        {
            return _configBindDal.GetChannelStructInfo(sn);
        }
    }
}
