using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Data;
using System.Data.SqlClient;
using Model;



namespace Service.DAL
{
    public class MessageDao
    {
        string strConnection = string.Empty;
        public MessageDao()
        {
            strConnection = Library.Common.GetConnectionString();
        }

        public DataSet SelectMessage(string msgType)
        {

            string strSql = "select top 5 * from CRM_Messages where Categories='" + msgType + "' order by issueDate desc";
            SqlParameter param = new SqlParameter("@msgType", SqlDbType.VarChar, 50);
            param.Value = msgType;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = param;
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql, sqlParams);
        }


        public DataSet SelectMessageAll(string msgType)
        {
            string strSql = "select  * from CRM_Messages where Categories=@msgType order by issueDate desc";
            SqlParameter param = new SqlParameter("@msgType", SqlDbType.VarChar, 50);
            param.Value = msgType;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = param;

            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql, sqlParams);
        }


        public DataSet GetMessage(int messageId)
        {
            string strSql = "select  * from CRM_Messages where id=@id order by issueDate desc";
            SqlParameter param = new SqlParameter("@id", SqlDbType.Int);
            param.Value = messageId;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = param;

            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql, sqlParams);
        }


        public int UpdateMessage(Model.Message msg)
        {

            string strSql = " UPDATE CRM_Messages SET Title = @title, Content = @content, Categories = @category, Type = @type WHERE ID = @id";
            SqlParameter paramTitle = new SqlParameter("@title", SqlDbType.VarChar, 200);
            paramTitle.Value = msg.Title;

            SqlParameter paramContent = new SqlParameter("@content", SqlDbType.VarChar, 2000);
            paramContent.Value = msg.Content;

            SqlParameter paramCategory = new SqlParameter("@category", SqlDbType.VarChar, 50);
            paramCategory.Value = msg.Category;

            SqlParameter paramType = new SqlParameter("@type", SqlDbType.VarChar, 50);
            paramType.Value = msg.Type;

            SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int, 4);
            paramId.Value = msg.Id;

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = paramTitle;
            sqlParams[1] = paramContent;
            sqlParams[2] = paramCategory;
            sqlParams[3] = paramType;
            sqlParams[4] = paramId;

            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);

        }

        public int InsertMessage(Model.Message msg)
        {
            string strSql = "INSERT INTO CRM_Messages( Title, Content, Categories, Type, IssueDate)VALUES (@title,@content,@category,@type,getdate())";

            SqlParameter paramTitle = new SqlParameter("@title", SqlDbType.VarChar, 200);
            paramTitle.Value = msg.Title;

            SqlParameter paramContent = new SqlParameter("@content", SqlDbType.VarChar, 2000);
            paramContent.Value = msg.Content;


            SqlParameter paramCategory = new SqlParameter("@category", SqlDbType.VarChar, 50);
            paramCategory.Value = msg.Category;


            SqlParameter paramType = new SqlParameter("@type", SqlDbType.VarChar, 50);
            paramType.Value = msg.Type;

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = paramTitle;
            sqlParams[1] = paramContent;
            sqlParams[2] = paramCategory;
            sqlParams[3] = paramType;

            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);

        }




        public DataSet Search(string category, string key) {

            string strSql = "select * from CRM_Messages where Categories=@cagegory  and ( content like '%'+@key+'%' or Title like '%'+@key+'%')";

            SqlParameter paramCategory = new SqlParameter("@cagegory", SqlDbType.VarChar, 200);
            paramCategory.Value = category;

            SqlParameter paramKey = new SqlParameter("@key", SqlDbType.VarChar, 2000);
            paramKey.Value = key;

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = paramCategory;
            sqlParams[1] = paramKey;

            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql, sqlParams);




        }


        public int DeletMessage(int id) {

            string strSql = "delete  from CRM_Messages where id=@id";
            SqlParameter paramId = new SqlParameter("@id", SqlDbType.Int);
            paramId.Value = id;

          
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = paramId;


            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);
           

        
        }

    }
}
