using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 元件信息，统一模板
    /// </summary>
    public class FeatureItem
    {
        public FeatureItem()
        {
            CalcFrequency = new Dictionary<string, float>();
        }
        /// <summary>
        /// key（元件ID）
        /// </summary>
        public int FKey { get; set; }
        /// <summary>
        /// 元件类型
        /// </summary>
        public int FType { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string FMark { get; set; }
        /// <summary>
        /// 参考转速
        /// </summary>
        public float FInSpeed { get; set; }
        /// <summary>
        /// 输出转速
        /// </summary>
        public float FOutSpeed { get; set; }
        /// <summary>
        /// 显示参考转速
        /// </summary>
        public string FInSpeedDisplay
        {
            get
            {
                if (this.Parent != null)
                    return Parent.FOutSpeedDisplay;
                return "--";
            }
        }
        /// <summary>
        /// 显示输出转速
        /// </summary>
        public string FOutSpeedDisplay { get; set; }

        /// <summary>
        /// 存储对象
        /// </summary>
        public object Tag { get; set; }
        /// <summary>
        /// 关联测点名称集
        /// </summary>
        public string FParaNames { get; set; }
        /// <summary>
        /// 父级特征项
        /// </summary>
        public FeatureItem Parent { get; set; }

        /// <summary>
        /// 获取特征频率
        /// </summary>
        public Dictionary<string, float> CalcFrequency { get; set; }
        /// <summary>
        /// 故障所引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float this[string key]
        {
            get
            {
                try
                {
                    if (CalcFrequency.ContainsKey(key.ToLower()))
                        return CalcFrequency[key.ToLower()];
                }
                catch (System.Exception ex)
                {
                   
                }
                return -1f;
            }
            set
            {
                CalcFrequency[key.ToLower()] = value;
            }
        }
    }
}
