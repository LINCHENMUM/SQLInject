using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class UpdateNews : System.Web.UI.Page
{
    SqlData da = new SqlData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlParameter messageid = new SqlParameter("@messageid", SqlDbType.Int, 4);
            messageid.Value = Convert.ToInt32(Request["id"]);

            SqlParameter[] parasms = new SqlParameter[1];
            parasms[0] = messageid;
            //DataSet ds = da.datesetExecute("select * from CRM_Messages where id='" + messageid + "'", "tbNews");
            string sqlstring = "select * from CRM_Messages where id=@messageid";

            DataSet ds = da.datesetExecute(sqlstring, "tbNews",parasms);
            
            DataRow[] rows = ds.Tables["tbNews"].Select();
            foreach (DataRow dr in rows)
            {
                this.txtNewsTitle.Text = dr["title"].ToString();
                this.txtNewsContent.Text = dr["content"].ToString();
                this.labTitle.Text = dr["Categories"].ToString();

                switch (dr["type"].ToString())
                {
                    case "SQL注入研究":
                        this.dlstNewsType.SelectedIndex = 1;
                        break;
                    case "参数化编程":
                        this.dlstNewsType.SelectedIndex = 2;
                        break;
                    case "对象化编程":
                        this.dlstNewsType.SelectedIndex = 3;
                        break;
                    case "存储过程防御":
                        this.dlstNewsType.SelectedIndex = 4;
                        break;
                    case "全局防御":
                        this.dlstNewsType.SelectedIndex = 5;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string CRMconnString = WebConfigurationManager.AppSettings["ConnenctionStr"];
        SqlConnection conn = new SqlConnection(CRMconnString);
        try
        {
            conn.Open();

            //string sqlstring = " UPDATE CRM_Messages SET Title = '" + this.txtNewsTitle.Text + "', Content = '" + this.txtNewsContent.Text + "', Categories = '" + this.labTitle.Text.Trim() + "', Type = '" + this.dlstNewsType.SelectedValue.ToString() + "' WHERE ID = '" + Request.QueryString["id"] + "'";
            string sqlstring = " UPDATE CRM_Messages SET Title = @title, Content = @content, Categories = @category, Type = @type WHERE ID = @id";
            SqlCommand sqlcmd = new SqlCommand(sqlstring, conn);

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
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 4);
            id.Value = Request.QueryString["id"];
            sqlcmd.Parameters.Add(id);

            int restult = Convert.ToInt32(sqlcmd.ExecuteNonQuery());
            if (restult > 0)
                this.lblMessage.Text = "信息修改成功！";
            else
                this.lblMessage.Text = "信息修改失败！";

            conn.Close();
            conn.Dispose();

            //SqlDataReader sdr = da.ExecuteRead(sqlstring);
            //lblMessage.Text = "信息修改成功！";
        }
        catch
        {


        }
        finally {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.txtNewsContent.Text = "";
        this.txtNewsTitle.Text = "";
        Response.Redirect("~/Admin/NewsManage.aspx");
    }
}
