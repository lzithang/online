using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
    public interface IMeterageLibDal : IBaseRepository<MeterageLib>
    {
        /// <summary>
        /// ����sn��ȡ����������Ϣ
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        List<MeterageLibModel> GetMeterageLibListBySn(int sn);

    }
}
