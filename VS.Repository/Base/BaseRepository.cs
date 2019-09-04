using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VS.Common;

namespace VS.Repository
{
    public class BaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        protected SqlSugarClient Db { get; set; }

        /// <summary>
        /// 实体连接对象
        /// </summary>
        protected SimpleClient<TEntity> DbEntity { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseRepository()
        {
            ClientInfo clientInfo =  CallContext.GetData<ClientInfo>("clientInfo");
            string ip = AppsettingsHelper.GetConfigString("DataBase", "Ip");
            string account = AppsettingsHelper.GetConfigString("DataBase", "Account");
            string password = AppsettingsHelper.GetConfigString("DataBase", "Password");
            string connectionStr = $"Database='{clientInfo.Database}';Data Source='{ip}';User Id='{account}';Password='{password}';";
            //string connectionStr = AppsettingsHelper.GetConfigString("DataBase", "ConnectionString");
            DbContext.Init(connectionStr);
            Db = DbContext.GetContext().Db;
            DbEntity = new SimpleClient<TEntity>(Db);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity QueryById(object id, bool isCache = false)
        {
            return Db.Queryable<TEntity>().WithCacheIF(isCache).InSingle(id);
        }

        /// <summary>
        /// 获取全部实体
        /// </summary>
        /// <returns></returns>
        public List<TEntity> QueryList()
        {
            return Db.Queryable<TEntity>().ToList();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere)
        {
            return Db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return Db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToList();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToList();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return Db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList();
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere, string strOrderByFileds)
        {
            return Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList();
        }


        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFileds)
        {
            return Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList();
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="totalNumber">总页码</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds,
            ref int totalNumber)
        {
            return Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize, ref totalNumber);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="totalNumber">总页码</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds,
          ref int totalNumber)
        {
            return Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize, ref totalNumber);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件拉姆达表达式</param>
        /// <param name="totalNumber">总页码</param>
        /// <param name="intPageIndex">默认第一页</param>
        /// <param name="intPageSize">默认每页数20</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public List<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, ref int totalNumber,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return Db.Queryable<TEntity>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .WhereIF(whereExpression != null, whereExpression)
            .ToPageList(intPageIndex, intPageSize, ref totalNumber);
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertEntity(TEntity obj)
        {
            var id = Db.Insertable<TEntity>(obj).ExecuteReturnBigIdentity();
            return (int)id;
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="list"></param>
        public bool InsertEntityList(List<TEntity> list)
        {
            return Db.Insertable<TEntity>(list).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 插入实体 根据表名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool InsertEntity(TEntity entity,string tableName)
        {
            return Db.Insertable(entity).AS(tableName).ExecuteCommand() >0;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool UpdateEntity(TEntity obj)
        {
            return Db.Updateable(obj).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public bool DeleteById(object id)
        {
            return Db.Deleteable<TEntity>(id).ExecuteCommand()>0;
        }

    }
}
