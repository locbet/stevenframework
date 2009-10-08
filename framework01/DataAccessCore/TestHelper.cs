using System;
using System.Data;

namespace DataAccessCore
{
    /// <summary>
    /// 
    /// </summary>
    public class TestHelper : Base.BaseHelper
    {
        private static ConnString.TestRapTierConnString conn = new DataAccessCore.ConnString.TestRapTierConnString();
        public TestHelper()
            : base(conn)
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
