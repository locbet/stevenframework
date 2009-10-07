using System;
using System.Data;

namespace DataAccessCore
{
    public class TestHelper : Base.BaseHelper
    {
        private ConnString.TestConnString connstringclass;
        public TestHelper()
        {
            connstringclass = new ConnString.TestConnString();
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
        ~TestHelper()
        {
            this.Close();
        }
    }
}
