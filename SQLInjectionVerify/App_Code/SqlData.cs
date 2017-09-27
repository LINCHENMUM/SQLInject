using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public class SqlData
{
    private SqlConnection con;
    private SqlCommand cmd;
    private SqlDataAdapter da;
    
    public SqlData()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        con = new SqlConnection(ConfigurationManager.AppSettings["ConnenctionStr"]);
        //private static string conStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
       con.Open();
    }
    // 下载于www.mycodes.net 

    public bool ExecuteSQL(string str)
    {
        cmd = new SqlCommand(str, con);
        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            throw (new Exception(e.Message));
            return false;
        }

        finally
        {
            con.Close();
        }
    }


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:检查 SQL 查询是否存在安全漏洞")]
    public DataSet ExecuteDateSet(string str)
    {
        try
        {
            cmd = new SqlCommand(str, con);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            con.Close();
        }
    }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:检查 SQL 查询是否存在安全漏洞")]
    public DataSet datesetExecute(string str, string tableName, SqlParameter[] parasms)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = str;
            foreach (SqlParameter item in parasms)
            {
                cmd.Parameters.Add(item);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, tableName);
            return ds;
        }
        finally
        {
            con.Close();
        }
    }
    public DataSet datesetExecute(string str, string tableName)
    {
        try
        {            
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds, tableName);
            return ds;
        }
        finally
        {
            con.Close();
        }
    }

    public SqlDataReader ExecuteRead(string str)
    {
        cmd = new SqlCommand(str, con);
        SqlDataReader sdr = cmd.ExecuteReader();
        return sdr;
        con.Close();
    }
}