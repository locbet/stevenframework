namespace DataAccessCore.ConnString
{
    /// <summary>
    /// 数据库连接类
    /// </summary>
    public class TestRapTierConnString : Base.AbsConnString
    {
        public TestRapTierConnString()
        {

        }
        private string _connectionstring;
        public override string ConnectionString
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
