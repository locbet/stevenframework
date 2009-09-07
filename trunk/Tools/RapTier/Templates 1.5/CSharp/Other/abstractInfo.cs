using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace STN.Entity
{
    public abstract class abstractInfo
    {
        /// <summary>
        /// 设置reader数据到该对象
        /// </summary>
        /// <param name="reader"></param>
        public abstract void SetValue(IDataReader reader);
    }
}
