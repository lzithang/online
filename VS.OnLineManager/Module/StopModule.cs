using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Common;
using VS.IService;

namespace VS.OnLineManager
{
    public  class StopModule:IStopModule
    {
        /// <summary>
        /// 下位机模块信息
        /// </summary>
        private ClientInfo _clientInfo;
        private DataOaStopQueue _dataOaStopQueue;
        private Dictionary<string, bool> _isStop;
        public StopModule()
        {
            _clientInfo = CallContext.GetData<ClientInfo>("clientInfo");
            _dataOaStopQueue = RedisHelper.Get<DataOaStopQueue>($"{_clientInfo.Database}DataStop");
            _isStop = RedisHelper.Get<Dictionary<string, bool>>($"{_clientInfo.Database}IsStop");
            if (_dataOaStopQueue == null)
            {
                _dataOaStopQueue = new DataOaStopQueue();
            }

            if(_isStop == null)
            {
                _isStop = new Dictionary<string, bool>();
            }

        }

        /// <summary>
        /// 验证停机状态
        /// </summary>
        /// <param name="dataOaList"></param>
        /// <param name="meterage"></param>
        /// <param name="machineStopList"></param>
        /// <returns></returns>
        public bool ValidateStop(List<DataOa> dataOaList, Meterage meterage,List<MachineStop> machineStopList)
        {
            string key = $"A{meterage.AreaId}M{meterage.McId}";
            MachineStop machineStop = machineStopList.FirstOrDefault(m => m.AreaId == meterage.AreaId && m.McId == meterage.McId && m.ParId == meterage.ParId && m.DirId == meterage.DirId);
            if(machineStop == null)
            {
                bool isStop = _isStop.GetValueOrDefault(key);
                if (isStop)
                {
                    return true;
                }
                return false;
            }
            
            //更新停机状态
            Queue<DataOa> dataOaQueue = _dataOaStopQueue.DataQueue.GetValueOrDefault(key); 
            if (dataOaQueue == null)
            {
                dataOaQueue = new Queue<DataOa>();
            }
            foreach (DataOa item in dataOaList)
            {
                dataOaQueue.Enqueue(item);
            }

            if(dataOaQueue.Count >= 5)
            {
                while (dataOaQueue.Count > 5)
                {
                    dataOaQueue.Dequeue();
                }
                _dataOaStopQueue.DataQueue[key] = dataOaQueue;
                List<DataOa> validateData = dataOaQueue.ToList();
                for (int i = 0; i < validateData.Count; i++)
                {
                    double value = 0;
                    if (machineStop.MsType == "Vel") { value = validateData[i].OaVel; }
                    else if (machineStop.MsType == "Acc") { value = validateData[i].OaAcc; }
                    else if (machineStop.MsType == "Disp") { value = validateData[i].OaDisp; }
                    if(machineStop.MsValue < value)
                    {
                        _isStop[key] = false;
                        SaveCache();
                        return false;
                    }
                }
                _isStop[key]= true;
                SaveCache();
                return true;
            }

            _dataOaStopQueue.DataQueue[key] = dataOaQueue;
            SaveCache();
            return false;
        }

        public void SaveCache()
        {
            RedisHelper.Set($"{_clientInfo.Database}DataStop",_dataOaStopQueue);
            RedisHelper.Set($"{_clientInfo.Database}IsStop",_isStop);
        }

    }

    public class DataOaStopQueue
    {
        public DataOaStopQueue()
        {
            DataQueue = new Dictionary<string, Queue<DataOa>>();
        }

        public Dictionary<string,Queue<DataOa>> DataQueue { get; set; }
    }

}
