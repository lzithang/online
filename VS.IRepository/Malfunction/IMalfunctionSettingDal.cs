using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IMalfunctionSettingDal:IBaseRepository<MalfunctionSetting>
	{
        /// <summary>
        /// 根据数据Id 获取诊断项，诊断项为公式
        /// </summary>
        /// <param name="msrId">数据源Id</param>
        /// <returns></returns>
        List<MalfunctionSetting> GetMalfunctionSettingListByMsrId(int msrId);

    }
}
