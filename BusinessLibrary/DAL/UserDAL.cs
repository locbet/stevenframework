using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLibrary.Core;
using System.Data;

namespace BusinessLibrary.DAL
{
    public class UserDAL
    {
        public int CheckLogin(string username, string password, string email)
        {
            string sql1 = "select count(*) from T_User";
            string sql2 = "update dbo.T_User set email=@email where username=@username and password=@password";
            int i;
            using (DBHelper testsql = new DBHelper())
            {
                i = (int)testsql.ExecuteScalar(sql1);
            }
            using (DBHelper tsql = new DBHelper())
            {
                i = i + (int)tsql.ExecuteScalar(sql1);
                IDbDataParameter[] pams = new IDbDataParameter[]{
                tsql.CreateParameter("@username", username),
                tsql.CreateParameter("@password",password),
                tsql.CreateParameter("@email",email)};
                for (int j = 0; j < 1000; j++)
                {
                    tsql.SetParameter(pams[2], i.ToString());
                    i = i + tsql.ExecuteNonQuery(sql2, pams);
                }
            }
            return i;
        }
        public void UpdateLogin(string username)
        {
            string sql = "update T_user set abs";
            IDbDataParameter p = DBStaticHelper.CreateParameter("@abc", 1);
            DBStaticHelper.ExecuteNonQuery(CommandType.Text, sql, p);
        }
    }
}
