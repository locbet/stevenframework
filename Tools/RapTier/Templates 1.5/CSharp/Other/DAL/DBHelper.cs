using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace STN.Data.SqlServer.Base
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public sealed class DBHelper : IDisposable
    {
        private SqlConnection _connection;
        private static readonly string _connectionString = Common.DBConfig.DBConn;

        public DBHelper()
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = _connectionString;
            _connection.Open();

        }
        #region IDisposable 成员

        /// <summary>
        ///关闭连接
        /// </summary>
        private void Close()
        {
            if (null != _connection)
            {
                _connection.Close();
            }
        }
        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            Close();
            if (null != _connection)
            {
                _connection.Dispose();
            }
        }

        #endregion

        #region  执行简单SQL语句
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="trans"></param>
        /// <param name="condition"></param>
        private void PrepareCommand(SqlCommand cmd, SQLCondition condition)
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            cmd.Connection = _connection;
            cmd.CommandText = condition.CommandText.ToString();
            if (condition.ParamsCount > 0)
            {
                List<SqlParameter> paramList = condition.CommandParams;
                foreach (SqlParameter parm in paramList)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="condition">SQLcondition</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteNonQuerySql(SQLCondition condition)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, condition);
                int rows = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return rows;
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                this.Close();
                throw new Exception(E.Message);
            }
        }


        /// <summary>
        /// 执行 SQL语句,返回结果集中的第一行，第一列的值
        /// 执行一条计算查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="cmdParms"></param>
        /// <returns>第一行，第一列的值</returns>
        public object ExecuteScalarSql(SQLCondition condition)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, condition);
                object result = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return result;
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                this.Close();
                throw new Exception(E.Message);
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(SQLCondition condition)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, condition);
                SqlDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                this.Close();
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(SQLCondition condition)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, condition);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    this.Close();
                    throw new Exception(ex.Message);
                }
                return ds;
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public DataTable QueryDataTable(SQLCondition condition)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, condition);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    cmd.Parameters.Clear();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    this.Close();
                    throw new Exception(ex.Message);
                }
                return dt;
            }

        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public void ExecuteSqlTran(List<SQLCondition> conditionList)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            SqlTransaction tx = _connection.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                foreach (SQLCondition condition in conditionList)
                {
                    PrepareCommand(cmd, condition);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    tx.Commit();
                }
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                tx.Rollback();
                this.Close();
                throw new Exception(E.Message);
            }
        }
        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlDataReader returnReader;
            SqlCommand command = BuildQueryCommand(_connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            DataSet dataSet = new DataSet();
            using (SqlDataAdapter sqlDA = new SqlDataAdapter())
            {
                sqlDA.SelectCommand = BuildQueryCommand(_connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                return dataSet;
            }

        }


        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {

            int result;
            SqlCommand command = BuildIntCommand(_connection, storedProcName, parameters);
            rowsAffected = command.ExecuteNonQuery();
            result = (int)command.Parameters["ReturnValue"].Value;
            return result;

        }


        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }        
        #endregion

    }
}
