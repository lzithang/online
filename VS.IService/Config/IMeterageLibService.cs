using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IService
{
    public interface IMeterageLibService : IBaseService<MeterageLib>
    {
        /// <summary>
        /// 根据sn获取测量参数信息
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        List<MeterageLibModel> GetMeterageLibListBySn(int sn);

    }
}
