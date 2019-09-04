using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;


namespace VS.Service
{
    public class MeterageLibService : BaseService<MeterageLib>, IMeterageLibService
    {
        private IMeterageLibDal _meterageLibDal { get; set; }
        public MeterageLibService(IMeterageLibDal meterageLibDal)
        {
            _meterageLibDal = meterageLibDal;
            BaseDal = _meterageLibDal;
        }

        /// <summary>
        /// 根据sn获取测量参数信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public List<MeterageLibModel> GetMeterageLibListBySn(int sn)
        {
            return _meterageLibDal.GetMeterageLibListBySn(sn);
        }

    }
}
