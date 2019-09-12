using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IRepository
{
	public interface IMalfunctionSettingDal:IBaseRepository<MalfunctionSetting>
	{
        /// <summary>
        /// ��������Id ��ȡ���������Ϊ��ʽ
        /// </summary>
        /// <param name="msrId">����ԴId</param>
        /// <returns></returns>
        List<MalfunctionSetting> GetMalfunctionSettingListByMsrId(int msrId);

    }
}
