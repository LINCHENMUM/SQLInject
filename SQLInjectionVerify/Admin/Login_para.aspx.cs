﻿using System;
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

public partial class Login : System.Web.UI.Page
{
    SqlData da = new SqlData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.verifyCode.Text = Convert.ToString(GetCode());
        }
    }
    public int GetCode()
    {
        Random rand = new Random();
        int code = rand.Next(1000, 9999);
        return code; 
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        string strUsername = Request.Form["username"];
        string strPassword = Request.Form["pwd"];
        string sqlstring = "select * from CRM_User where Name=@Username and PassWord=@Password";

        SqlConnection dbcon = new SqlConnection(ConfigurationManager.AppSettings["ConnenctionStr"]);
        dbcon.Open();
        SqlCommand cmd = new SqlCommand(sqlstring, dbcon);

        SqlParameter Username = new SqlParameter("@Username", SqlDbType.VarChar, 50);
        Username.Value = strUsername.Trim();
        cmd.Parameters.Add(Username);

        SqlParameter Password = new SqlParameter("@Password", SqlDbType.VarChar, 50);
        Password.Value = strPassword.Trim();
        cmd.Parameters.Add(Password);

        SqlDataReader sdr = cmd.ExecuteReader();
       
        //SqlDataReader sdr = da.ExecuteRead(sqlstring);
        sdr.Read();

        if (sdr.HasRows)
        {
            if (this.checkCode.Text.Trim() == this.verifyCode.Text.Trim())
            {
                Response.Redirect("Index.aspx");
            }
            else
            {
                Response.Write("<script>alert('输入的验证码错误!')</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('输入的姓名或密码错误误！请重新输入')</script>");
            Server.Transfer("Login.aspx");
        }
    }
    protected void cancel_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();location='javascript:history.go(-1)';</script>");
    }
}
