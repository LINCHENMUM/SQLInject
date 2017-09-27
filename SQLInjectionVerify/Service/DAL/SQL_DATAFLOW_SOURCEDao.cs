using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;
using Library;

namespace Service.DAL
{
    class SQL_DATAFLOW_SOURCEDao
    {
        string strConnection = string.Empty;
        public SQL_DATAFLOW_SOURCEDao()
        {
            strConnection = Library.Common.GetConnectionString();
        }

        public int insertDATAFLOW(Model.SQL_DATAFLOW_SOURCE dataflow)
        {
            string strSql = "INSERT INTO SQL_DATAFLOW_SOURCE( DESTINATIONOPERAND, OPCODE,SOURCEOPERAND,TREELEVEL,CODELINENO,FILENAME,TREENAME)VALUES (@DESTINATIONOPERAND,@OPCODE,@SOURCEOPERAND,@TREELEVEL,@CODELINENO,@FILENAME,@TREENAME)";
            //string strSql = "INSERT INTO SQL_DATAFLOW( DESTINATIONOPERAND, OPCODE,SOURCEOPERAND,TREELEVEL,CODELINENO,FILENAME,TREENAME)VALUES (@DESTINATIONOPERAND,@OPCODE,@SOURCEOPERAND,@TREELEVEL,@CODELINENO,@FILENAME,@TREENAME)";

            SqlParameter paramDESTINATIONOPERAND = new SqlParameter("@DESTINATIONOPERAND", SqlDbType.NVarChar, 200);
            paramDESTINATIONOPERAND.Value = dataflow.Destinationoperand;

            SqlParameter paramOPCODE = new SqlParameter("@OPCODE",SqlDbType.NVarChar,50);
            paramOPCODE.Value = dataflow.Opcode;

            SqlParameter paramSOURCEOPERAND = new SqlParameter("@SOURCEOPERAND", SqlDbType.NVarChar, 200);
            paramSOURCEOPERAND.Value = dataflow.Sourceoperand;

            SqlParameter paramTREELEVEL = new SqlParameter("@TREELEVEL", SqlDbType.Int);
            paramTREELEVEL.Value = dataflow.Treelevel;

            SqlParameter paramCODELINENO = new SqlParameter("@CODELINENO", SqlDbType.Int);
            paramCODELINENO.Value = dataflow.Codelineno;

            SqlParameter paramFILENAME = new SqlParameter("@FILENAME", SqlDbType.NVarChar,800);
            paramFILENAME.Value = dataflow.Filename;

            SqlParameter paramTREENAME = new SqlParameter("@TREENAME", SqlDbType.NVarChar, 100);
            paramTREENAME.Value = dataflow.Treename;

            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = paramDESTINATIONOPERAND;
            sqlParams[1] = paramOPCODE;
            sqlParams[2] = paramSOURCEOPERAND;
            sqlParams[3] = paramTREELEVEL;
            sqlParams[4] = paramCODELINENO;
            sqlParams[5] = paramFILENAME;
            sqlParams[6] = paramTREENAME;

            return SqlHelper.ExecuteNonQuery(strConnection, System.Data.CommandType.Text, strSql, sqlParams);
        }
    }
}
