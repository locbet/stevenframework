using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static UserInfo SetValue(IDataReader reader)
        {
            UserInfo info = new UserInfo();
            int u_idColumnIndex = reader.GetOrdinal("userid");
            int u_usernameColumnIndex = reader.GetOrdinal("username");
            int u_passwordColumnIndex = reader.GetOrdinal("password");

            info.ID = reader.GetGuid(u_idColumnIndex);
            info.UserName = Convert.ToString(reader.GetValue(u_usernameColumnIndex));
            info.PassWord = Convert.ToString(reader.GetValue(u_passwordColumnIndex));
            return info;
        }
    }
}