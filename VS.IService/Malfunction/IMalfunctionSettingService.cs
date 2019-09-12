using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IService
{
    public interface IMalfunctionSettingService : IBaseService<MalfunctionSetting>
    {
        /// <summary>
        /// ��������Id ��ȡ���������Ϊ��ʽ
        /// </summary>
        /// <param name="msrId">����ԴId</param>
        /// <returns></returns>
        List<MalfunctionSetting> GetMalfunctionSettingListByMsrId(int msrId);

    }
}
