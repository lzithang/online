using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Common
{
    public class AppsettingsHelper
    {
        private static IConfigurationRoot configuration;

        /// <summary>
        /// 加载配置文件
        /// </summary>
        static AppsettingsHelper()
        {
            var builder = new ConfigurationBuilder();
            configuration = builder.AddJsonFile("appsettings.json", false, true).Build();
        }

        /// <summary>
        /// 获取配置节点内容
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetConfigString(params string[] section)
        {
            try
            {
                string val = string.Empty;
                foreach (string item in section)
                {
                    val += item + ":";
                }
                return configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
