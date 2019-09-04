using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace VS.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity QueryById(object id, bool isCache = false);

        /// <summary>
        /// 获取全部实体
        /// </summary>
        /// <returns></returns>
        List<TEntity> QueryList();

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(string strWhere);

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(string strWhere, string strOrderByFileds);


        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFileds);

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="totalNumber">总页码</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds,
            ref int totalNumber);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <param name="totalNumber">总页码</param>
        /// <returns>数据列表</returns>
        List<TEntity> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds,
          ref int totalNumber);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件拉姆达表达式</param>
        /// <param name="totalNumber">总页码</param>
        /// <param name="intPageIndex">默认第一页</param>
        /// <param name="intPageSize">默认每页数20</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        List<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, ref int totalNumber,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null);

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        int InsertEntity(TEntity obj);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="list"></param>
        bool InsertEntityList(List<TEntity> list);

        /// <summary>
        /// 插入实体 根据表名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool InsertEntity(TEntity entity, string tableName);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool UpdateEntity(TEntity obj);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        bool DeleteById(object id);
    }
}
