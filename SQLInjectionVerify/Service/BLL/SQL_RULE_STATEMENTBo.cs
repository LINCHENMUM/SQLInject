using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Service.BLL
{
    public class SQL_RULE_STATEMENTBo
    {
        private DAL.SQL_RULE_STATEMENTDao dao;
        public SQL_RULE_STATEMENTBo()
        {
            dao = new DAL.SQL_RULE_STATEMENTDao();
        }
        public DataSet SelectOpcodeSecurity(string strOpcode)
        {
            return dao.SelectRuleSecurity(strOpcode);
        }

        public DataSet SelectAllRule()
        {
            return dao.SelectAllRule();
        }
        public DataSet SelectUserInputRule()
        {
            return dao.SelectUserInputRule();
        }
    }
}
