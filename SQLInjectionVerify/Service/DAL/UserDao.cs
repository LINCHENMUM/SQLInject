using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Data.SqlClient;
using Model;
using System.Data;

namespace Service.DAL
{
    public class UserDao
    {

        string strConnection = string.Empty;
        public UserDao()
        {
            strConnection = Library.Common.GetConnectionString();
        }


        public DataSet Select(string strName, string strPwd)
        {
            string strSql = "select * from CRM_User where Name=@name and PassWord=@pwd ";

            SqlParameter param = new SqlParameter("@name", SqlDbType.NVarChar, 50);
            param.Value = strName;
            SqlParameter param2 = new SqlParameter("@pwd", SqlDbType.NVarChar, 50);
            param2.Value = strPwd;
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = param;
            sqlParams[1] = param2;

            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql, sqlParams);

        }


        public DataSet GetAllUser()
        {
            string strSql = "select * from CRM_User";
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql);
        }

        public int InsertUser(Model.User user) {

            string strSql = "insert into CRM_User (Name,PassWord) values(@addname,@addpwd)";

            SqlParameter paramName = new SqlParameter("@addname", SqlDbType.VarChar, 50);
            paramName.Value = user.Name;
            SqlParameter paramPwd = new SqlParameter("@addpwd", SqlDbType.VarChar, 50);
            paramPwd.Value = user.Password;

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = paramName;
            sqlParams[1] = paramPwd;
           

            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);
      
        }


        public int DeleteUser(int userId)
        {
            string strSql = "Delete from CRM_User where ID=@id";
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
            param.Value = userId;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = param;

            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);

        }


        public int UpdateUser(Model.User user)
        {
            string strSql = "Update CRM_User set Name=@name,PassWord=@pwd  where ID=@id";

            SqlParameter paramName = new SqlParameter("@name", SqlDbType.VarChar, 200);
            paramName.Value = user.Name;
            SqlParameter paramPwd = new SqlParameter("@pwd", SqlDbType.VarChar, 2000);
            paramPwd.Value = user.Password;
            SqlParameter paramId = new SqlParameter("@id", SqlDbType.VarChar, 50);
            paramId.Value = user.Id;

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = paramName;
            sqlParams[1] = paramPwd;
            sqlParams[2] = paramId;

            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);

        }




    }
}
