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
    public class SQL_DATAFLOWDao
    {
        string strConnection = string.Empty;
        public SQL_DATAFLOWDao()
        {
            strConnection = Library.Common.GetConnectionString();
        }
        //根据文件名、源操作数和树数据流的树名查找数据流树信息
        public DataSet SelectDestination(string strSourceoperand,string strFileName,string strTreeName)
        {
            string strSql = "select * from SQL_DATAFLOW with(nolock) where SOURCEOPERAND =@sourceoperand and FILENAME=@filename and TREENAME=@treename";

            SqlParameter param = new SqlParameter("@sourceoperand", SqlDbType.NVarChar, 200);
            param.Value = strSourceoperand;

            SqlParameter paramfile = new SqlParameter("@filename", SqlDbType.NVarChar, 800);
            paramfile.Value = strFileName;

            SqlParameter paramTree = new SqlParameter("@treename", SqlDbType.VarChar, 100);
            paramTree.Value = strTreeName;

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = param;
            sqlParams[1] = paramfile;
            sqlParams[2] = paramTree;
            return SqlHelper.ExecuteDataset(strConnection,System.Data.CommandType.Text,strSql,sqlParams);
        }
        //找到所有待检测的文件
        public DataSet SelectAllScanFile()
        {
            string strFileSql = "select FILENAME from SQL_DATAFLOW WITH(NOLOCK) group by FILENAME order by FILENAME";
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strFileSql);
        }

        //找到同一个文件中所有的初始化用户输入
        public DataSet SelectFirstSource(string strFileName)
        {
            string strSourceSql = "select TREENAME from SQL_DATAFLOW WITH(NOLOCK) where FILENAME=@filename group by TREENAME order by TREENAME";
            SqlParameter paramFileName = new SqlParameter("@filename", SqlDbType.NVarChar, 800);
            paramFileName.Value = strFileName;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = paramFileName;
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSourceSql,sqlParams);
        }
        //找到同一个用户输入的所有树节点
        public DataSet SelectAllTreeExceptRoot(string strFileName,string strTreeName)
        {
            string strSourceSql = "select * from SQL_DATAFLOW WITH(NOLOCK) where FILENAME=@filename and TREENAME=@treename order by ID ASC";

            SqlParameter paramFileName = new SqlParameter("@filename", SqlDbType.NVarChar, 800);
            paramFileName.Value = strFileName;

            SqlParameter paramTree = new SqlParameter("@treename", SqlDbType.VarChar, 100);
            paramTree.Value = strTreeName;

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = paramFileName;
            sqlParams[1] = paramTree;

            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSourceSql,sqlParams);
        }
        public DataSet SelectAllAST()
        {
            //string strAST = "select * from SQL_DATAFLOW WITH(NOLOCK) ORDER BY ID ASC";
            string strAST = "select * from SQL_DATAFLOW WITH(NOLOCK) ORDER BY ID ASC";
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strAST);

        }
    }
}
