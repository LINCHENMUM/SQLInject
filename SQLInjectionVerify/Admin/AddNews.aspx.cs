using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

using System.Data.SqlClient;
public partial class AddNews : System.Web.UI.Page
{
    SqlData da = new SqlData();
    protected void Page_Load(object sender, EventArgs e)
    {
       // int Category = Convert.ToInt32((Request["id"].ToString()));
        //object Category1 = Request.QueryString["id"];
        //Response.Write(Category1);
       
            int Category = 0;

            if (Request.QueryString.HasKeys())
            {


               Category = Convert.ToInt32(Request.QueryString["id"].ToString());
            }



            switch (Category)
            {
                case 1:
                    this.labTitle.Text = "SQL注入研究";
                    break;
                case 2:
                    this.labTitle.Text = "参数化编程";
                    break;
                case 3:
                    this.labTitle.Text = "对象化编程";
                    break;
                case 4:
                    this.labTitle.Text = "存储过程防御";
                    break;
                case 5:
                    this.labTitle.Text = "全局防御";
                    break;
            }
        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //string title = this.txtNewsTitle.Text.Trim();
        //string content = this.txtNewsContent.Text.Trim();
        //string category = this.labTitle.Text.Trim();
        //string type = this.dlstNewsType.SelectedValue.ToString();
        string CRMconnString = WebConfigurationManager.AppSettings["ConnenctionStr"];
        SqlConnection conn = new SqlConnection(CRMconnString);
       
        try
        {
            conn.Open();
            SqlCommand sqlcmd = new SqlCommand("INSERT INTO CRM_Messages( Title, Content, Categories, Type, IssueDate)VALUES (@title,@content,@category,@type,getdate())", conn);
            SqlParameter title = new SqlParameter("@title", SqlDbType.VarChar, 200);
            title.Value = this.txtNewsTitle.Text.Trim();
            sqlcmd.Parameters.Add(title);
            SqlParameter content = new SqlParameter("@content", SqlDbType.VarChar, 2000);
            content.Value = this.txtNewsContent.Text.Trim();
            sqlcmd.Parameters.Add(content);
            SqlParameter category = new SqlParameter("@category", SqlDbType.VarChar, 50);
            category.Value = this.labTitle.Text.Trim();
            sqlcmd.Parameters.Add(category);
            SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar, 50);
            type.Value = this.dlstNewsType.SelectedValue.ToString();
            sqlcmd.Parameters.Add(type);

            int restult = Convert.ToInt32(sqlcmd.ExecuteNonQuery());
            if (restult > 0)
                this.lblMessage.Text = "信息添加成功！";
            else
                this.lblMessage.Text = "信息添加失败！";

            conn.Close();
            conn.Dispose();
        }
        catch
        {
            Response.Redirect("~/Admin/Index.aspx");
        }
        finally
        {

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        //string sqlstring = "INSERT INTO CRM_Messages( Title, Content, Categories, Type, IssueDate)VALUES ('" + this.txtNewsTitle.Text.Trim() + "', '" + this.txtNewsContent.Text.Trim() + "', '" + this.labTitle.Text.Trim()+ "', '" + this.dlstNewsType.SelectedValue.ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
        //SqlDataReader sdr = da.ExecuteRead(sqlstring);
        //this.lblMessage.Text = "信息添加成功！";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.txtNewsContent.Text = "";
        this.txtNewsTitle.Text = "";
        Response.Redirect("~/Admin/Index.aspx");
    }
}
