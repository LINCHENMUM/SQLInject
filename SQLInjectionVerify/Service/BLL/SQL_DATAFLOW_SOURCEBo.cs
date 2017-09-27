using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Service.BLL
{
 public   class SQL_DATAFLOW_SOURCEBo
    {
        private DAL.SQL_DATAFLOW_SOURCEDao dao;
        public SQL_DATAFLOW_SOURCEBo()
        {
            dao = new DAL.SQL_DATAFLOW_SOURCEDao();
        }

        public int insertDATAFLOW(Model.SQL_DATAFLOW_SOURCE dataflow)
        {
            return dao.insertDATAFLOW(dataflow);

        }

    }
}
