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

using System.Data.SqlClient;
public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlData da = new SqlData();
        string strShiShi = "select top 5 * from CRM_Messages where Categories='SQL注入研究' order by issueDate desc";
        string strEconomic = "select top 5 * from CRM_Messages where Categories='参数化编程' order by issueDate desc";
        SqlDataReader dar = da.ExecuteRead(strShiShi);
        dlstShiShi.DataSource = dar;
        dlstShiShi.DataKeyField = "id";
        dlstShiShi.DataBind();
        dar.Close();

        SqlDataReader sdrEco = da.ExecuteRead(strEconomic);
        dlstEconomic.DataSource = sdrEco;
        dlstEconomic.DataKeyField = "id";
        dlstEconomic.DataBind();
        sdrEco.Close();
        
        //Test
        /*
        Response.Write(Request.ServerVariables["QUERY_STRING"] + "----" + "QUERY_STRING" + "<BR>");
        Response.Write(Request.ServerVariables["SERVER_NAME"] + "----" + "SERVER_NAME" + "<BR>");
        Response.Write(Request.ServerVariables["url"] + "----" + "ServerVariables-URL" + "<BR>");
        Response.Write(Request.ServerVariables["SERVER_PORT"] + "----" + "SERVER_PORT" + "<BR>");
        Response.Write(Request.ServerVariables["REMOTE_ADDR"] + "----" + "REMOTE_ADDR" + "<BR>");
        Response.Write(Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + "----" + "HTTP_X_FORWARDED_FOR" + "<BR>");
        Response.Write(Request.ServerVariables["REMOTE_PORT"] + "----" + "REMOTE_PORT" + "<BR>");

        Response.Write(Server.HtmlEncode(Request.ServerVariables["http_user_agent"]) + "----" + "http_user_agent-HtmlEncode" + "<BR>");
        Response.Write(Server.HtmlEncode(Request.UserAgent.ToString()) + "----" + "Request.UserAgent" + "<BR>");
        Response.Write(Server.HtmlEncode(Request.Headers.ToString()) + "----" + "Request.headers" + "<BR>");

        Response.Write(Server.HtmlEncode(Request.UserHostAddress.ToString()) + "----" + "Request.UserHostAddress" + "<BR>");
        Response.Write(Server.HtmlEncode(Request.UserHostName.ToString()) + "----" + "Request.UserHostName" + "<BR>");

        Response.Write(Request.Url + "----" + "Request.Url " + "<BR>");
        Response.Write(Request.RawUrl + "----" + "Request.RawUrl " + "<BR>");
        Response.Write(Request.UrlReferrer + "----" + "Request.UrlReferrer " + "<BR>");
        Response.Write(Request.Url.Port + "----" + "Request.Url port " + "<BR>");
        Response.Write(Request.Url.Scheme + "----" + "Request.Url scheme " + "<BR>");
         */
     
    }

    protected void dlstShiShi_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int @id = Convert.ToInt32(dlstShiShi.DataKeys[e.Item.ItemIndex].ToString());
        SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 4);
        param.Value = @id;
        //Response.Write("<script language=javascript>window.open('NewsDetail.aspx?id=" + id + "','','_blank')</script>");
        Response.Write("<script language=javascript>window.open('NewsDetail.aspx?id=" + param.Value + "','','_blank')</script>");
    }
    protected void dlstEconomic_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int @id = Convert.ToInt32(dlstEconomic.DataKeys[e.Item.ItemIndex].ToString());
        SqlParameter param = new SqlParameter("@id", SqlDbType.Int, 4);
        param.Value = @id;
        //Response.Write("<script language=javascript>window.open('NewsDetail.aspx?id=" + id + "','','width=520,height=260')</script>");
        Response.Write("<script language=javascript>window.open('NewsDetail.aspx?id=" + param.Value + "','','width=520,height=260')</script>");
    }
}
