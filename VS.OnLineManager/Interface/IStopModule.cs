using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.OnLineManager
{
    public interface IStopModule
    {
        /// <summary>
        /// 验证停机状态
        /// </summary>
        /// <param name="dataOaList"></param>
        /// <param name="meterage"></param>
        /// <param name="machineStopList"></param>
        /// <returns></returns>
        bool ValidateStop(List<DataOa> dataOaList, Meterage meterage, List<MachineStop> machineStopList);

        void SaveCache();
    }
}
