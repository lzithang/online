using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Repository
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    public class DbContext
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string ConfigString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        private static  DbType DbType { get; set; }

        /// <summary>
        /// 数据库DB
        /// </summary>
        public SqlSugarClient Db { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接</param>
        private DbContext(bool blnIsAutoCloseConnection)
        {
            if (string.IsNullOrEmpty(ConfigString))
                throw new ArgumentNullException("数据库连接字符串为空");
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigString,
                DbType = DbType,
                IsAutoCloseConnection = blnIsAutoCloseConnection,
                IsShardSameThread = true,
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = true
                }
            });
            //用来打印Sql方便你调式    
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(sql + "\r\n" +
                //Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
            };
        }

        /// <summary>
        /// 获取DbContext
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接</param>
        /// <returns></returns>
        public static DbContext GetContext(bool blnIsAutoCloseConnection =true)
        {
            return new DbContext(blnIsAutoCloseConnection);
        }

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="configStr">数据库连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        public static void Init(string configStr,DbType dbType = DbType.MySql)
        {
            ConfigString = configStr;
            DbType = DbType;
        }
    }
}
