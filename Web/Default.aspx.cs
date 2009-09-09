using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLibrary;
using Entity;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UserBLL userBLL = new UserBLL();
            UserInfo info = userBLL.CheckLogin();
            this.lblID.Text = info.ID.ToString();
            this.lblUserName.Text = info.UserName;
            this.lblPassWord.Text = info.PassWord;
            this.lblMemo.Text = info.Memo;

            this.lblUserCount.Text = userBLL.GetUserCount().ToString();
        }
    }
}
