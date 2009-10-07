/**************************************************
 * 功能:抽象类实现数据连接字符串的相关信息
 * 创建时间：2009-9-30
 * 创建者:steven
 * ************************************************
 */
namespace DataAccessCore.Base
{
    /// <summary>
    /// 数据库连接抽象类类
    /// </summary>
    public abstract class AbsConnString
    {
        /// <summary>
        /// 超时默认时间为30
        /// </summary>
        private int _timeOut = 30;
        /// <summary>
        /// 超时时间
        /// </summary>
        public virtual int TimeOut
        {
            get
            {
                return _timeOut;
            }
        }
        /// <summary>
        /// 数据库的连接字符串
        /// </summary>
        public abstract string ConnectionString { get; }
    }
}
