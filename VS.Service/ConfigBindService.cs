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
        /// ��ȡChannelStruct���Ͳ�����Ϣ
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public List<ConfigBindModel> GetChannelStructInfo(int sn)
        {
            return _configBindDal.GetChannelStructInfo(sn);
        }
    }
}
