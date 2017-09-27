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
    public class SQL_SCANRESULTDao
    {
        string strConnection = string.Empty;
        public SQL_SCANRESULTDao()
        {
            strConnection = Library.Common.GetConnectionString();
        }

        public int InsertScanResult(Model.SQL_SCANRESULT scanresult)
        {
            string strSql = "INSERT INTO SQL_SCANRESULT( SQLINJECTSOURCE, CODELINENO,FILENAME,SCANDATE,SCANRESULT,DATAFLOW,BATCH)VALUES (@SQLINJECTSOURCE, @CODELINENO,@FILENAME,getdate(),@SCANRESULT,@DATAFLOW,@BATCH)";

            SqlParameter paramSQLINJECTSOURCE = new SqlParameter("@SQLINJECTSOURCE", SqlDbType.NVarChar, 200);
            paramSQLINJECTSOURCE.Value = scanresult.Sqlinjectsource;

            SqlParameter paramCODELINENO = new SqlParameter("@CODELINENO", SqlDbType.Int);
            paramCODELINENO.Value = scanresult.Codelineno;

            SqlParameter paramFILENAME = new SqlParameter("@FILENAME", SqlDbType.NVarChar,200);
            paramFILENAME.Value = scanresult.Filename;

            SqlParameter paramSCANRESULT = new SqlParameter("@SCANRESULT", SqlDbType.VarChar, 30);
            paramSCANRESULT.Value = scanresult.Scanresult;

            SqlParameter paramDATAFLOW = new SqlParameter("@DATAFLOW", SqlDbType.VarChar, 800);
            paramDATAFLOW.Value = scanresult.Dataflow;

            SqlParameter paramBATCH = new SqlParameter("@BATCH", SqlDbType.Int);
            paramBATCH.Value = scanresult.Batch;
          
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = paramSQLINJECTSOURCE;
            sqlParams[1] = paramCODELINENO;
            sqlParams[2] = paramFILENAME;
            sqlParams[3] = paramSCANRESULT;
            sqlParams[4] = paramDATAFLOW;
            sqlParams[5] = paramBATCH;

            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);

        }

        public DataSet SelectScanResultAll(int intBatch)
        {
            string strSql = "select SQLINJECTSOURCE,CODELINENO,FILENAME,SCANDATE,SCANRESULT,DATAFLOW from SQL_SCANRESULT with(nolock) where BATCH=@BATCH order by SCANDATE desc";
            SqlParameter paramBATCH = new SqlParameter("@BATCH", SqlDbType.Int);
            paramBATCH.Value = intBatch;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = paramBATCH;

            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql,sqlParams);
        }

        public DataSet SelectMaxBatch()
        {
            string strSql = "select isnull(max(BATCH),0) as BATCH from SQL_SCANRESULT with(nolock)";
            return SqlHelper.ExecuteDataset(strConnection, System.Data.CommandType.Text, strSql);
        }
    }
}
