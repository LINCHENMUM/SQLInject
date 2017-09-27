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
    public class SQL_RULE_STATEMENTDao
    {
        string strConnection = string.Empty;
        public SQL_RULE_STATEMENTDao()
        {
            strConnection = Library.Common.GetConnectionString();
        }
        public DataSet SelectRuleSecurity(string strOpcode)
        {
            string strSql = "select SECURITYTYPE from SQL_RULE_STATEMENT with(nolock) where STATEMENT=@statement order by statement asc";

            SqlParameter param = new SqlParameter("@statement", SqlDbType.NVarChar, 50);
            param.Value = strOpcode;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = param;
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql, sqlParams);
        }

        public DataSet SelectAllRule()
        {
            string strSql = "select * from SQL_RULE_STATEMENT with(nolock) order by ID asc";
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql);
        }
        public DataSet SelectUserInputRule()
        {
            string strSql = "select * from SQL_RULE_STATEMENT with(nolock) where STATEMENTTYPE='UserInput' order by ID asc";
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql);
        }
    }
}
