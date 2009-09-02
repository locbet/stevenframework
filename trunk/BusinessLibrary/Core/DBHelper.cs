using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessLibrary.Core
{
    public class ConnStringClass : IConnString
    {
        public ConnStringClass()
        {

        }
        private string _connectionstring;
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionstring))
                {
                    _connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
                }
                return _connectionstring;
            }
        }
    }
    public class DBHelper : BaseHelper
    {
        private ConnStringClass connstringclass;
        public DBHelper()
        {
            connstringclass = new ConnStringClass();
            base.InitConection(connstringclass.ConnectionString);
        }
        public void SetParameter(IDataParameter parameter, object value)
        {
            if (value == null)
            {
                value = (object)DBNull.Value;
            }
            parameter.Value = value;
        }
        ~DBHelper()
        {
            this.Close();
        }
    }
    public class DBStaticHelper : BaseStaticHelper<ConnStringClass>
    {

    }
}
