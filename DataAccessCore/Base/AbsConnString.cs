
namespace DataAccessCore.Base
{
    public abstract class AbsConnString
    {
        private int _timeOut = 30;
        public virtual int TimeOut
        {
            get
            {
                return _timeOut;
            }
        }
        public abstract string ConnectionString { get; }
    }
}
