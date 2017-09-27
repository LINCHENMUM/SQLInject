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
// 下载于www.mycodes.net 
public partial class ShowNewsInformation : System.Web.UI.Page
{
    SqlData da = new SqlData();
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();
    }
    protected void Bind()
    {
        int count = Convert.ToInt32(Request.QueryString["id"]);
        switch (count)
        {
            case 1:
                Label4 .Text  = "SQL注入研究";
                break;
            case 2:
                Label4.Text = "参数化编程";
                break;

        }
        int currentPage = Convert.ToInt32(this.currentPage.Text);
        string @params = Label4.Text.Trim();
        SqlParameter param = new SqlParameter("@params", SqlDbType.VarChar, 50);
        param.Value = @params;

        PagedDataSource pds = new PagedDataSource();
       //string sqlstring = "select * from CRM_Messages where Categories='" +param.Value + "' order by issueDate desc";
        string sqlstring = "select * from CRM_Messages where Categories='"+param.Value+"' order by issueDate desc";
        //DataSet ds = da.datesetExecute(sqlstring, "tbNews");
        DataSet ds = da.datesetExecute(sqlstring, "tbNews");
        pds.DataSource = ds.Tables["tbNews"].DefaultView;
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.CurrentPageIndex = currentPage - 1;
        this.firstPage.Enabled = true;
        this.frontPage.Enabled = true;
        this.nextPage.Enabled = true;
        this.lastPage.Enabled = true;

        if (currentPage == 1)
        {
            this.firstPage.Enabled = false;
            this.frontPage.Enabled = false;
        }
        if (currentPage == pds.Count)
        {
            this.nextPage.Enabled = false;
            this.lastPage.Enabled = false;
        }
        this.totalPage.Text = Convert.ToString(pds.PageCount);
        this.dlstNews.DataSource = pds;
        this.dlstNews.DataKeyField = "id";
        this.dlstNews.DataBind();
    }
    
    protected void firstPage_Click(object sender, EventArgs e)
    {
        this.currentPage.Text = "1";
        this.Bind();
    }

    protected void frontPage_Click(object sender, EventArgs e)
    {
        this.currentPage.Text = Convert.ToString(Convert.ToInt32(this.currentPage.Text) - 1);
        this.Bind();
    }
    protected void nextPage_Click(object sender, EventArgs e)
    {
        this.currentPage.Text = Convert.ToString(Convert.ToInt32(this.currentPage.Text) + 1);
    }
    protected void lastPage_Click(object sender, EventArgs e)
    {
        this.currentPage.Text = Convert.ToString(Convert.ToInt32(this.totalPage.Text));
        this.Bind();
    }
    protected void ldNewsSort_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int id = Convert.ToInt32(dlstNews.DataKeys[e.Item.ItemIndex].ToString());
        Response.Redirect("NewsDetail.aspx?id=" + id + "");
    }
}
