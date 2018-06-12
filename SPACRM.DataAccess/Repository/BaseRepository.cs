using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity.Entities;
using SPACRM.Common;
using SPACRM.Entity.PageSearch;
using Vulcan.Framework.DBConnectionManager;
using Dapper;
using System.Data;

namespace SPACRM.DataAccess.Repository
{
    public abstract class BaseRepository : Vulcan.Repository.BaseRepository
    {
        public BaseRepository()
            : base(ConnectionStringHelper.GetValueByKey(AppConfig.MainDbKey))
        {
        }
        public BaseRepository(string dbkey)
            : base(ConnectionStringHelper.GetValueByKey(dbkey))
        {
        }

        protected PagedList<T> PageGet<T>(PageView view, string sqlColumns, string sqlTable, string sqlCondition, string sqlPk, string sqlOrder)
        {
            if (string.IsNullOrEmpty(sqlOrder))
            {
                sqlOrder = " ORDER BY " + sqlPk;
            }
            //edit by xuanye 2014.9
            //else
            //{
            //    sqlOrder = " ORDER BY " + view.SortName + " " + view.SortOrder;
            //}           
            var p = new DynamicParameters();
            int total = 0;
            p.Add("@SQLPARAMS", sqlCondition, DbType.String, ParameterDirection.Input, null);
            p.Add("@PAGESIZE", view.PageSize, DbType.Int32, ParameterDirection.Input, null);
            p.Add("@PAGEINDEX", view.PageIndex, DbType.Int32, ParameterDirection.Input, null);
            p.Add("@SQLTABLE", sqlTable, DbType.String, ParameterDirection.Input, null);
            p.Add("@SQLCOLUMNS", sqlColumns, DbType.String, ParameterDirection.Input, null);
            p.Add("@SQLPK", sqlPk, DbType.String, ParameterDirection.Input, null);
            p.Add("@SQLORDER", sqlOrder, DbType.String, ParameterDirection.Input, null);
            p.Add("@Count", total, DbType.Int32, ParameterDirection.Output, null);

            var list = base.SPQuery<T>("sp_pageselect", p);

            PagedList<T> pList = new PagedList<T>();

            if (view.PageIndex == 0)
            {
                pList.Total = p.Get<int>("@Count");
            }
            pList.DataList = list;

            pList.PageIndex = view.PageIndex;
            pList.PageSize = view.PageSize;
            return pList;
        }
        /// <summary>
        /// 构造分页查询语句
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="orderByClause">排序规则</param>
        /// <param name="tableName">表名</param>
        /// <param name="columnsName">字段名称</param>
        /// <returns>SQL语句</returns>
        public string BuildPageSql(int pageSize, int pageIndex, string whereClause, string orderByClause, string tableName, string columnsName)
        {
            int startNumber = pageIndex * pageSize + 1;
            int endNumber = pageSize * (pageIndex + 1);
            string sql = string.Format("SELECT * " +
                " FROM (SELECT TOP {1} {0},ROW_NUMBER() OVER (ORDER BY {2}) AS rownumber" +
                " FROM {3} where 1=1 {4} ORDER BY {2}) PageTable" +
                " WHERE rownumber <= {1} and rownumber >= {5}",
                columnsName, endNumber, orderByClause, tableName, whereClause, startNumber);
            return sql;
        }
        /// <summary>
        /// 分页查询 for SQL语句
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="view">分页属性</param>
        /// <param name="sqlColumns">查询字段</param>
        /// <param name="sqlTable">表名</param>
        /// <param name="sqlCondition">条件</param>
        /// <param name="param">条件所需参数</param>
        /// <param name="sqlPk">主键列名</param>
        /// <param name="sqlOrder">排序规则</param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns></returns>
        protected PagedList<T> PageGetBySQL<T>(PageView view, string sqlColumns, string sqlTable, string sqlCondition, object param, string sqlPk, string sqlOrder)
        {
            PagedList<T> pList = new PagedList<T>();
            int totalCount = -1;
            if (view.PageIndex == 0)
            {
                string totalSql = string.Format(" select count(1) from {0} where 1=1 {1} ;", sqlTable, sqlCondition);
                totalCount = base.Get<int>(totalSql, param);
            }

            if (string.IsNullOrEmpty(sqlOrder))
            {
                sqlOrder = " ORDER BY " + sqlPk;
            }
            int pageStartIndex = view.PageIndex;
            int currentPageCount = view.PageSize;
            string sql = BuildPageSql(currentPageCount, pageStartIndex, sqlCondition, sqlPk, sqlTable, sqlColumns);
            pList.DataList = base.Query<T>(sql, param);
            pList.Total = totalCount;
            pList.PageIndex = view.PageIndex;
            pList.PageSize = view.PageSize;
            return pList;
        }

        protected List<T> Query<T>(string sql, int timeOut, object paras)
        {
            List<T> list = null;
            using (ConnectionManager mgr = GetConnection())
            {
                list = mgr.Connection.Query<T>(sql, paras, mgr.Transaction, false, timeOut, CommandType.Text).ToList();
            }
            return list;
        }
        
    }
}
