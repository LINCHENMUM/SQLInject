using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Service.BLL
{
    public class SQL_SCANRESULTBo
    {
        private DAL.SQL_SCANRESULTDao dao;
        public SQL_SCANRESULTBo()
        {
            dao = new DAL.SQL_SCANRESULTDao();
        }

        public int InsertScanResult(Model.SQL_SCANRESULT scanresult)
        {
            return dao.InsertScanResult(scanresult);

        }
        public DataSet SelectScanResultAll(int intBatch)
        {
            return dao.SelectScanResultAll(intBatch);
        }

        public DataSet SelectMaxBatch()
        {
            return dao.SelectMaxBatch();
        }
    }
}
