namespace DataAccessCore.ConnString
{
    public class TestConnString:Base.IConnString
    {
        public TestConnString()
        {

        }
        private string _connectionstring;
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionstring))
                {
                    _connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["TestRapTier"].ConnectionString;
                }
                return _connectionstring;
            }
        }
    }
}
