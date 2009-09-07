using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace STN.Data.SqlServer.Base
{
    /// <summary>
    /// SQL语句处理类 
    /// </summary>
    public class SQLCondition
    {
        private StringBuilder _cmdText;
        private List<SqlParameter> _cmdParams;
        private List<string> _whereparts;

        #region result
        
        /// <summary>
        /// get [SQL参数]
        /// </summary>
        public List<SqlParameter> CommandParams
        {
            get
            {
                return _cmdParams;
            }
        }
        /// <summary>
        /// get or set [SQL语句]
        /// </summary>
        public StringBuilder CommandText
        {
            get
            {
                return _cmdText;
            }
            set
            {
                _cmdText = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public SQLCondition()
        {
            _cmdText = new StringBuilder();
            _cmdParams = new List<SqlParameter>();
            _whereparts = new List<string>();
        }

        public int WherePartCount
        {
            get
            {
                return _whereparts.Count;
            }
        }


        /// <summary>
        /// get [判断参数个数]
        /// </summary>
        public int ParamsCount
        {
            get
            {
                return _cmdParams.Count;
            }
        }

        /// <summary>
        /// 添加部分条件语句作为临时存取
        /// </summary>
        /// <param name="partCondition"></param>
        public void AddWherePart(string partCondition)
        {
            if (!string.IsNullOrEmpty(partCondition))
            {
                _whereparts.Add(partCondition);
            }
        }

        /// <summary>
        /// 获得当前所有wherepart中用and连接的结果字符串
        /// </summary>
        /// <returns></returns>
        public string GetWherePartAll()
        {
            if (WherePartCount > 0)
            {
                StringBuilder sqlparts = new StringBuilder();
                sqlparts.Append(" (").Append(_whereparts[0]).Append(")");
                for (int i = 1; i < WherePartCount; i++)
                {
                    sqlparts.Append(" and (").Append(_whereparts[i]).Append(")");
                }
                return sqlparts.ToString();
            }
            return "";
        }


        /// <summary>
        /// 添加参数        
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="value">参数值</param>
        public void AddcmdParam(string paramName, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = paramName;
            p.Value = (null == value ? DBNull.Value : value);
            _cmdParams.Add(p);
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="value">参数值</param>
        public void AddcmdParam(string paramName, DbType dbType, object value)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = paramName;
            p.DbType = dbType;
            p.Value = (null == value ? DBNull.Value : value);
            _cmdParams.Add(p);
        }

        #region 暂时不用
        ///// <summary>
        ///// 添加参数
        ///// </summary>
        ///// <param name="param"></param>
        //public void AddcmdParam(SqlParameter param)
        //{
        //    _cmdParams.Add(param);
        //}
        ///// <summary>
        ///// 添加参数列表
        ///// </summary>
        ///// <param name="paramList"></param>
        //public void AddcmdParam(List<SqlParameter> paramList)
        //{
        //    _cmdParams.AddRange(paramList);
        //}

        ///// <summary>
        ///// 制造参数
        ///// </summary>
        ///// <param name="paramName"></param>
        ///// <param name="dbType"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static SqlParameter MakeParam(string paramName, DbType dbType, object value)
        //{
        //    SqlParameter parameter = new SqlParameter();
        //    parameter.ParameterName = paramName;
        //    parameter.DbType = dbType;
        //    parameter.Value = null == value ? DBNull.Value : value;
        //    return parameter;
        //}

        ///// <summary>
        ///// 生成存储过程参数
        ///// </summary>
        ///// <param name="ParamName">存储过程名称</param>
        ///// <param name="DbType">参数类型</param>
        ///// <param name="Size">参数大小</param>
        ///// <param name="Direction">参数方向</param>
        ///// <param name="Value">参数值</param>
        ///// <returns>新的 parameter 对象</returns>
        /////
        //public static SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        //{
        //    #region
        //    SqlParameter param;
        //    if (Size > 0)
        //        param = new SqlParameter(ParamName, DbType, Size);
        //    else
        //        param = new SqlParameter(ParamName, DbType);

        //    param.Direction = Direction;
        //    if (!(Direction == ParameterDirection.Output && Value == null))
        //        param.Value = Value;

        //    return param;
        //    #endregion
        //}
        ///// <summary>
        ///// 传入输入参数
        ///// </summary>
        ///// <param name="ParamName">存储过程名称</param>
        ///// <param name="DbType">参数类型</param></param>
        ///// <param name="Size">参数大小</param>
        ///// <param name="Value">参数值</param>
        ///// <returns>新的 parameter 对象</returns>
        //public static SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        //{
        //    return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        //}

        ///// <summary>
        ///// 传入返回值参数
        ///// </summary>
        ///// <param name="ParamName">存储过程名称</param>
        ///// <param name="DbType">参数类型</param>
        ///// <param name="Size">参数大小</param>
        ///// <returns>新的 parameter 对象</returns>
        //public static SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        //{
        //    return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        //}
        #endregion
    }
}
