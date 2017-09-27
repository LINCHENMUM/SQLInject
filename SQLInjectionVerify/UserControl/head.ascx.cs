using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UserControl_head : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.dateLabel.Text = "今天是：" + System.DateTime.Now.ToString("yyyy年MM月dd日");
    }
   
}
