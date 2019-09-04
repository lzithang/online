using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.OnLineManager
{
    public interface IOaAlarmModule
    {
        /// <summary>
        /// 保存缓存数据
        /// </summary>
        void SaveCache();

        /// <summary>
        /// 验证报警
        /// </summary>
        /// <param name="dataOaList"></param>
        /// <param name="meterage"></param>
        void ValidateAlarm(List<DataOa> dataOaList, Meterage meterage);
    }
}
