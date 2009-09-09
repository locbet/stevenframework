using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLibrary.Core;
using System.Data;
using Entity;

namespace BusinessLibrary.DAL
{
    public class UserDAL
    {
        public UserInfo CheckLogin()
        {
            string sql = "select * from [User] where [UserName]=@username";
            using (DBHelper db = new DBHelper())
            {
                IDbDataParameter p = db.CreateParameter("@username", SqlDbType.NVarChar, 50, "steven");
                IDataReader reader = db.ExecuteReader(sql, p);
                if (reader.Read())
                {
                    return UserInfo.SetValue(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        public int GetUserCount()
        {
            string sql = "select count(*) from [User]";
            return Convert.ToInt32(DBStaticHelper.ExecuteScalar(CommandType.Text, sql));
        }
    }
}
