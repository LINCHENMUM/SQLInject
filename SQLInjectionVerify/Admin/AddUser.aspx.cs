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
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class AddUser : System.Web.UI.Page
{
    SqlData da = new SqlData();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void add_Click(object sender, EventArgs e)
    {
        string CRMconnString = WebConfigurationManager.AppSettings["ConnenctionStr"];
        SqlConnection conn = new SqlConnection(CRMconnString);
        try
        {
            conn.Open();
            string stringSqlquery = "select * from CRM_User where Name=@name and PassWord=@pwd";
            SqlCommand sqlcmdquery = new SqlCommand(stringSqlquery, conn);
            SqlParameter name = new SqlParameter("@name", SqlDbType.VarChar, 50);
            name.Value = this.username.Text.Trim();
            sqlcmdquery.Parameters.Add(name);
            SqlParameter pwd = new SqlParameter("@pwd", SqlDbType.VarChar, 50);
            pwd.Value = this.pwd.Text.Trim();
            sqlcmdquery.Parameters.Add(pwd);

            int restult = Convert.ToInt32(sqlcmdquery.ExecuteScalar());
            if (restult > 0)
            {
                this.lblMessage.Text = "该用户已存在";
            }
            else
            {
                string sqlstringadd = "insert into CRM_User (Name,PassWord) values(@addname,@addpwd)";
                SqlCommand sqlcmdadd = new SqlCommand(sqlstringadd, conn);
                SqlParameter addname = new SqlParameter("@addname", SqlDbType.VarChar, 50);
                addname.Value = this.username.Text.Trim();
                sqlcmdadd.Parameters.Add(addname);
                SqlParameter addpwd = new SqlParameter("@addpwd", SqlDbType.VarChar, 50);
                addpwd.Value = this.pwd.Text.Trim();
                sqlcmdadd.Parameters.Add(addpwd);
                int addrestult = Convert.ToInt32(sqlcmdadd.ExecuteNonQuery());
                if (addrestult > 0)
                    this.lblMessage.Text = "用户添加成功";
                else
                    this.lblMessage.Text = "用户添加失败";
            }
            conn.Close();
            conn.Dispose();
        }
        catch
        {
            this.lblMessage.Text = "用户添加失败";
        }
        finally {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
    protected void reset_Click(object sender, EventArgs e)
    {
        this.username.Text = "";
        this.pwd.Text = "";
        this.lblMessage.Text = "";
        Response.Redirect("~/Admin/Index.aspx");
    }
}
