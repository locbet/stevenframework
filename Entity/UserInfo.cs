// <fileinfo name="Entity\UserInfo.cs">
//		<copyright>
//			All rights reserved.
//		</copyright>
//		<remarks>
//			Do not change this source code manually. Changes to this file may 
//			cause incorrect behavior and will be lost if the code is regenerated.
//		</remarks>
//		<generator rewritefile="True">Steven</generator>
// </fileinfo>

using System;
using System.Data;

namespace Entity
{
    /// <summary>
    /// The base class for <see cref="UserRow"/> that 
    /// represents a record in the <c>User</c> table.
    /// </summary>
    /// <remarks>
    /// Do not change this source code manually. Update the <see cref="UserRow"/>
    /// class if you need to add or change some functionality.
    /// </remarks>
    public class UserInfo
    {
        private System.Guid _id;
        private string _userName;
        private string _passWord;
        private string _memo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfo"/> class.
        /// </summary>
        public UserInfo()
        {
            // EMPTY
        }

        /// <summary>
        /// Gets or sets the <c>ID</c> column value.
        /// </summary>
        /// <value>The <c>ID</c> column value.</value>
        public System.Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Gets or sets the <c>UserName</c> column value.
        /// </summary>
        /// <value>The <c>UserName</c> column value.</value>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// Gets or sets the <c>PassWord</c> column value.
        /// </summary>
        /// <value>The <c>PassWord</c> column value.</value>
        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        /// <summary>
        /// Gets or sets the <c>Memo</c> column value.
        /// This column is nullable.
        /// </summary>
        /// <value>The <c>Memo</c> column value.</value>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// Returns UserInfo of this instance.
        /// </summary>
        /// <returns>UserInfo of this instance.</returns>
        public static UserInfo SetValue(IDataReader reader)
        {
            UserInfo info = new UserInfo();
            int idColumnIndex = reader.GetOrdinal("ID");
            int userNameColumnIndex = reader.GetOrdinal("UserName");
            int passWordColumnIndex = reader.GetOrdinal("PassWord");
            int memoColumnIndex = reader.GetOrdinal("Memo");

            info.ID = reader.GetGuid(idColumnIndex);
            info.UserName = Convert.ToString(reader.GetValue(userNameColumnIndex));
            info.PassWord = Convert.ToString(reader.GetValue(passWordColumnIndex));
            if (!reader.IsDBNull(memoColumnIndex))
                info.Memo = Convert.ToString(reader.GetValue(memoColumnIndex));
            return info;
        }
    } // End of UserInfo class
} // End of namespace
