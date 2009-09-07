using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using STN.Entity;

namespace STN.Data.SqlServer.Base
{
    public abstract class ManageBase<EntityRow>
        where EntityRow : abstractInfo, new()
    {
        /// <summary>
        /// 根据reader生成相应实体数据
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private EntityRow ToEntityRow(IDataReader reader)
        {
            EntityRow single = null;
            while (reader.Read())
            {
                single = new EntityRow();
                single.SetValue(reader);
            }
            return single;
        }
        /// <summary>
        /// 根据reader列表数据生成相应实体数据列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<EntityRow> ToEntityRows(IDataReader reader)
        {
            List<EntityRow> list = new List<EntityRow>();
            while (reader.Read())
            {
                EntityRow tmp = new EntityRow();
                tmp.SetValue(reader);
                list.Add(tmp);
            }
            return list;
        }
        /// <summary>
        /// 用于带参数查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        protected List<EntityRow> Select(SQLCondition sqlCondition)
        {
            using (DBHelper dbh = new DBHelper())
            {
                return ToEntityRows(dbh.ExecuteReader(sqlCondition));
            }
        }
        /// <summary>
        /// 用于带参数查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        protected EntityRow Select_One(SQLCondition sqlCondition)
        {
            using (DBHelper dbh = new DBHelper())
            {
                return ToEntityRow(dbh.ExecuteReader(sqlCondition));
            }
        }
        /// <summary>
        /// 执行 SQL语句,返回结果集中的第一行，第一列的值
        /// 执行一条计算查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        protected object ExecuteScalar(SQLCondition sqlCondition)
        {
            using (DBHelper dbh = new DBHelper())
            {
                return dbh.ExecuteScalarSql(sqlCondition);
            }
        }
        /// <summary>
        /// 执行 SQL语句,返回结果影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        protected int ExecuteNonQuery(SQLCondition sqlCondition)
        {
            using (DBHelper dbh = new DBHelper())
            {
                return dbh.ExecuteNonQuerySql(sqlCondition);
            }
        }        
    }
}
