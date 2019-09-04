using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Model;
using VS.IRepository;
using VS.IService;

namespace VS.Service
{
    public class BaseService<TEntity>  where TEntity : class, new()
    {
        protected IBaseRepository<TEntity> BaseDal { get; set; }
        public bool DeleteById(object id)
        {
            return  BaseDal.DeleteById(id);
        }

        public int InsertEntity(TEntity obj)
        {
            return BaseDal.InsertEntity(obj);
        }

        public bool InsertEntityList(List<TEntity> list)
        {
            return BaseDal.InsertEntityList(list);
        }

        public TEntity QueryById(object id, bool isCache = false)
        {
            return BaseDal.QueryById(id,isCache);
        }

        public List<TEntity> QueryList()
        {
            return BaseDal.QueryList();
        }

        /// <summary>
        /// 插入实体 根据表名
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool InsertEntity(TEntity entity, string tableName)
        {
            return BaseDal.InsertEntity(entity, tableName);
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere)
        {
            return BaseDal.Query(strWhere);
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return BaseDal.Query(whereExpression);
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return BaseDal.Query(whereExpression, strOrderByFileds);
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
            return BaseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(string strWhere, string strOrderByFileds)
        {
            return BaseDal.Query(strWhere, strOrderByFileds);
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
            return BaseDal.Query(whereExpression, intTop, strOrderByFileds);
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
            return BaseDal.Query(strWhere, intTop, strOrderByFileds);
        }



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds,
            ref int totalNumber)
        {
            return BaseDal.Query(whereExpression, intPageIndex, intPageSize, strOrderByFileds, ref totalNumber);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public List<TEntity> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds,
          ref int totalNumber)
        {
            return BaseDal.Query(strWhere, intPageIndex, intPageSize, strOrderByFileds, ref totalNumber);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件拉姆达表达式</param>
        /// <param name="intPageIndex">默认第一页</param>
        /// <param name="intPageSize">默认每页数20</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        public List<TEntity> QueryPage(Expression<Func<TEntity, bool>> whereExpression, ref int totalNumber,
        int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return BaseDal.QueryPage(whereExpression, ref totalNumber, intPageIndex, intPageSize, strOrderByFileds);
        }

        public bool UpdateEntity(TEntity obj)
        {
            return BaseDal.UpdateEntity(obj);
        }
    }
}
