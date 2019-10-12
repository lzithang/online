using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using VS.Common;

namespace VS.OnLineManager
{
    public class FeatureExtraction
    {
        /// <summary>
        /// 频谱图信息
        /// </summary>
        private DataTwModel _waveData;

        /// <summary>
        /// 频谱图
        /// </summary>
        private List<float[]> _data = new List<float[]>();

        /// <summary>
        /// 诊断配置
        /// </summary>
        private MalfunctionSetting _setting;

        /// <summary>
        /// 剩余能量值下标
        /// </summary>
        private List<int> _residueIndex = new List<int>();

        private float _ratio =0.0f;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="waveData"></param>
        public FeatureExtraction(DataTwModel waveData)
        {
            _waveData = waveData;
            _ratio = waveData.DataHz / (float)waveData.DataLines;
            GetSpectrum();
            for (int i = 0; i < _waveData.Data.Length; i++)
            {
                _residueIndex.Add(i);
            }
        }

        /// <summary>
        /// 提取特征值
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public float ComputedFeature(MalfunctionSetting setting)
        {
            float tempF = 0.0f;
            try
            {
                _setting = setting;
                switch (setting.MmId)
                {
                    case 1: tempF = BandRMS(); break;
                    case 2: tempF = FreqPeak(); break;
                    case 3: tempF = FreqPeaksRMS(); break;
                    case 4: tempF = SBCenterPeak(); break;
                    case 5: tempF = SBCenterPeaksMax(); break;
                    case 6: tempF = SBsRMS(); break;
                    case 7: tempF = CenterPeakXBRMS(); break;
                    case 8: tempF = BandNoiseRMS(); break;
                    default: tempF = ResidueValue(); break;
                }
                if (float.IsNaN(tempF))
                    return -1;
                return tempF;
            }
            catch (Exception ex)
            {
                
            }
            return tempF;
        }

        private int FindIndex(float valueF)
        {
            List<float> list = new List<float>();
            for (int i = 0; i < _data.Count; i++)
                list.Add(_data[i][0]);
            return list.Select((d, i) =>
            {
                return new
                {
                    Value = d,
                    Index = i
                };
            }).OrderBy(x => Math.Abs(x.Value - valueF)).First().Index;
        }

        /// <summary>
        /// 查找峰值下标
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private int GetPeakIndex(string expression)
        {
            //固定频率不需要查找
            if (_setting.FindType == 2 && !expression.Contains("_"))
            {
                float value = CalcByJs(expression);
                //return FindIndex(value);
                for (int i = 0; i < _data.Count; i++)
                {
                    if (_data[i][0] > value)
                    {
                        return i;
                    }
                }
            }

            float start = 0;
            float end = 0;

            if (_setting.FindType == 1) //百分比查找
            {
                float value = CalcByJs(expression);
                start = value - value * _setting.MsRange / 100;
                end = value + value * _setting.MsRange / 100;
            }
            else if (_setting.FindType == 2) //频带查找
            {
                string[] value = expression.Split('_');
                start = CalcByJs(value[0]);
                end = CalcByJs(value[1]);
            }
            else if (_setting.FindType == 3) //谱线数查找
            {
                float value = CalcByJs(expression);
                start = value - _setting.MsLine * _ratio;
                end = value + _setting.MsLine * _ratio;
            }

            return GetMaxPeakIndex(start, end);
        }

        /// <summary>
        /// 查找峰值下标
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private int GetPeakIndex(float value)
        {
            //固定频率不需要查找
            if (_setting.FindType == 2)
            {
                for (int i = 0; i < _data.Count; i++)
                {
                    if (_data[i][0] < value)
                    {
                        return i;
                    }
                }
            }

            float start = 0;
            float end = 0;
            if (_setting.FindType == 1) //百分比查找
            {
                start = value - value * _setting.MsRange / 100;
                end = value + value * _setting.MsRange / 100;
            }
            else if (_setting.FindType == 3) //谱线数查找
            {
                start = value - _setting.MsLine * _ratio;
                end = value + _setting.MsLine * _ratio;
            }

            return GetMaxPeakIndex(start, end);
        }

        /// <summary>
        /// 寻找最高峰
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetMaxPeakIndex(float start, float end)
        {
            float temp = 0;
            int index = 0;
            for (int i = 0; i < _data.Count; i++)
            {
                if (_data[i][0] > start)
                {
                    if (_data[i][0] > end)
                    {
                        break;
                    }
                    if (temp < _data[i][1])
                    {
                        temp = _data[i][1];
                        index = i;
                    }
                }

            }
            return index;
        }

        /// <summary>
        /// 获取频谱图
        /// </summary>
        private void GetSpectrum()
        {
            for (int i = 0; i < _waveData.Data.Length; i++)
            {
                float[] temp = { _ratio * i, _waveData.Data[i] };
                _data.Add(temp);
            }
        }

        /// <summary>
        ///  公式计算
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static float CalcByJs(string expression)
        {
            return float.Parse(CalcHelper.CalcExpression(expression));
        }


        /// <summary>
        /// 均方根
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private float ValueRMS(List<float> data)
        {
            double value = 0;
            for (int i = 0; i < data.Count; i++)
            {
                value += Math.Pow(data[i], 2);
            }
            //value /= data.Count;
            return (float)Math.Sqrt(value);
        }

        /// <summary>
        /// 移除下标
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void RemoveIndex(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                _residueIndex.Remove(i);
            }
        }

        #region 特征值提取方法
        /// <summary>
        /// 频带RMS 方法1
        /// </summary>
        /// <returns></returns>
        private float BandRMS()
        {
            string[] str = _setting.CommonFormula.Split('_');
            int start = GetPeakIndex(str[0]);
            int end = GetPeakIndex(str[1]);
            List<float> list = new List<float>();
            for (int i = start; i <= end; i++)
            {
                list.Add(_data[i][1]);
            }
            RemoveIndex(start, end);
            return ValueRMS(list);
        }

        /// <summary>
        /// 峰值评估 方法2
        /// </summary>
        /// <returns></returns>
        private float FreqPeak()
        {
            string[] str = _setting.CommonFormula.Split(',', '，');
            int index = GetPeakIndex(str[0]);
            _residueIndex.Remove(index);
            return _data[index][1];
        }

        /// <summary>
        /// 峰值RMS 方法3
        /// </summary>
        /// <returns></returns>
        private float FreqPeaksRMS()
        {
            List<float> list = new List<float>();
            string[] str = _setting.CommonFormula.Split(',', '，');
            for (int i = 0; i < str.Length; i++)
            {
                int index = GetPeakIndex(str[i]);
                _residueIndex.Remove(index);
                list.Add(_data[index][1]);
            }
            return ValueRMS(list);
        }

        /// <summary>
        /// 中心频率比边带 方法4
        /// </summary>
        /// <returns></returns>
        private float SBCenterPeak()
        {
            string[] center = _setting.CenterFrequency.Split(',', '，');

            int centerIndex = GetPeakIndex(center[0]);
            float fre = CalcByJs(_setting.SidebandFrequency);
            float centerFre = _data[centerIndex][0];
            float centerValue = _data[centerIndex][1];
            int sbIndex1 = GetPeakIndex(centerFre + fre);
            int sbIndex2 = GetPeakIndex(centerFre - fre);
            float sbFre1 = _data[sbIndex1][1];
            float sbFre2 = _data[sbIndex2][1];
            float sbRatio1 = sbFre1 / centerValue;
            float sbRatio2 = sbFre2 / centerValue;
            RemoveIndex(sbIndex2, sbIndex1);
            return sbRatio1 > sbRatio2 ? sbRatio1 : sbRatio2;
        }

        /// <summary>
        /// 多个中心频率比边带Max 方法5
        /// </summary>
        /// <returns></returns>
        private float SBCenterPeaksMax()
        {
            string[] center = _setting.CenterFrequency.Split(',', '，');
            float fre = CalcByJs(_setting.SidebandFrequency);
            List<float> list = new List<float>();
            for (int i = 0; i < center.Length; i++)
            {
                int centerIndex = GetPeakIndex(center[i]);
                float centerFre = _data[centerIndex][0];
                float centerValue = _data[centerIndex][1];
                int sbIndex1 = GetPeakIndex(centerFre + fre);
                int sbIndex2 = GetPeakIndex(centerFre - fre);
                float sbFre1 = _data[sbIndex1][1];
                float sbFre2 = _data[sbIndex2][1];
                RemoveIndex(sbIndex2, sbIndex1);
                float sbRatio1 = sbFre1 / centerValue;
                float sbRatio2 = sbFre2 / centerValue;
                list.Add(sbRatio1 > sbRatio2 ? sbRatio1 : sbRatio2);
            }
            return list.Max();
        }

        /// <summary>
        /// 多个边带RMS 方法6
        /// </summary>
        /// <returns></returns>
        private float SBsRMS()
        {
            string[] center = _setting.CenterFrequency.Split(',', '，');
            string[] sb = _setting.SidebandFrequency.Split(',', '，');
            int centerIndex = GetPeakIndex(center[0]);
            float centerFre = _data[centerIndex][1];
            List<float> list = new List<float>();
            for (int i = 0; i < sb.Length; i++)
            {
                float fre = CalcByJs(sb[i]);
                int sbIndex1 = GetPeakIndex(centerFre + fre);
                int sbIndex2 = GetPeakIndex(centerFre - fre);
                RemoveIndex(sbIndex2, sbIndex1);
                float sbFre1 = _data[sbIndex1][1];
                float sbFre2 = _data[sbIndex2][1];
                list.Add(sbFre1);
                list.Add(sbFre2);
            }
            return ValueRMS(list);
        }

        /// <summary>
        /// 边带频段RMS 方法7
        /// </summary>
        /// <returns></returns>
        private float CenterPeakXBRMS()
        {
            List<float> list = new List<float>();
            string[] center = _setting.CenterFrequency.Split(',', '，');
            int centerIndex = GetPeakIndex(center[0]);
            float fre = CalcByJs(_setting.SidebandFrequency);
            float centerFre = _data[centerIndex][0];
            int end = GetPeakIndex(centerFre + fre);
            int start = GetPeakIndex(centerFre - fre);
            RemoveIndex(start, end);
            for (int i = start + 1; i < end; i++)
            {
                list.Add(_data[i][1]);
            }
            return ValueRMS(list);
        }

        /// <summary>
        /// 频带去谐频 方法8
        /// </summary>
        /// <returns></returns>
        private float BandNoiseRMS()
        {
            List<int> removeIndexList = new List<int>();
            List<float> list = new List<float>();
            string[] common = _setting.CommonFormula.Split('_');
            string[] remove = _setting.RemoveFrequency.Split(',', '，');
            int start = GetPeakIndex(common[0]);
            int end = GetPeakIndex(common[1]);
            RemoveIndex(start, end);
            for (int i = 0; i < remove.Length; i++)
            {
                removeIndexList.Add(GetPeakIndex(remove[i]));
            }
            for (int i = start + 1; i < end; i++)
            {
                if (removeIndexList.Contains(i))
                {
                    continue;
                }
                list.Add(_data[i][1]);
            }
            return ValueRMS(list);

        }

        /// <summary>
        /// 计算剩余能量值
        /// </summary>
        /// <returns></returns>
        private float ResidueValue()
        {
            List<float> list = new List<float>();
            foreach (int index in _residueIndex)
            {
                list.Add(_data[index][1]);
            }
            return ValueRMS(list);
        }
        #endregion

    }
}
