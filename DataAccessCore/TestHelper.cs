using System;
using System.Data;

namespace DataAccessCore
{
    public class TestHelper : Base.BaseHelper
    {
        private static ConnString.TestConnString connstringclass = new DataAccessCore.ConnString.TestConnString();
        public TestHelper()
            : base(connstringclass)
        {

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
