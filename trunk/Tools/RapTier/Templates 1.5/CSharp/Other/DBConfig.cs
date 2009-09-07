using System;
using System.Collections.Generic;
using System.Text;

namespace STN.Common
{
    public class DBConfig
    {
        /// <summary>
        /// 当前数据库连接字符串
        /// </summary>
        public static string DBConn
        {
            get
            {
#if DEBUG
                return System.Configuration.ConfigurationManager.ConnectionStrings["DebugConn"].ConnectionString;
#else
        return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["RealseConn"].ConnectionString;
#endif

            }
        }
    }

}
