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

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        Response.Write(Request.ServerVariables["QUERY_STRING"] + "----" + "QUERY_STRING" + "<BR>");
        Response.Write(Request.ServerVariables["SERVER_NAME"] + "----" + "SERVER_NAME" + "<BR>");
        Response.Write(Request.ServerVariables["url"] + "----" + "ServerVariables-URL" + "<BR>");
        Response.Write(Request.ServerVariables["SERVER_PORT"] + "----" + "SERVER_PORT" + "<BR>");
        Response.Write(Request.ServerVariables["REMOTE_ADDR"] + "----" + "REMOTE_ADDR" + "<BR>");
        Response.Write(Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + "----" + "HTTP_X_FORWARDED_FOR" + "<BR>");
        Response.Write(Request.ServerVariables["REMOTE_PORT"] + "----" + "REMOTE_PORT" + "<BR>");

        Response.Write(Request.ServerVariables["http_user_agent"] + "----" + "http_user_agent" + "<BR>");
        Response.Write(Request.UserAgent.ToString() + "----" + "Request.UserAgent" + "<BR>");
        Response.Write(Request.UserHostAddress.ToString() + "----" + "Request.UserHostAddress" + "<BR>");
        Response.Write(Request.UserHostName.ToString() + "----" + "Request.UserHostName" + "<BR>");

        Response.Write(Request.Url + "----" + "Request.Url " + "<BR>");
        Response.Write(Request.RawUrl + "----" + "Request.RawUrl " + "<BR>");
        Response.Write(Request.UrlReferrer + "----" + "Request.UrlReferrer " + "<BR>");
        Response.Write(Request.Url.Port + "----" + "Request.Url port " + "<BR>");
        Response.Write(Request.Url.Scheme + "----" + "Request.Url scheme " + "<BR>");
         */ 
    }
}
