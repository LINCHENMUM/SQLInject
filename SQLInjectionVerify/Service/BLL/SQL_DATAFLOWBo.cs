using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Service.BLL
{
    public class SQL_DATAFLOWBo
    {
        private DAL.SQL_DATAFLOWDao dao;
        public SQL_DATAFLOWBo()
        {
            dao=new DAL.SQL_DATAFLOWDao();
        }

        //根据文件名、源操作数和树数据流的树名查找数据流树信息
        public DataSet SelectDestinationOperand(string sourceOperand,string strFileName,string strTreeName)
        {

            return dao.SelectDestination(sourceOperand, strFileName, strTreeName);
        }

        //找到所有待检测的文件
        public DataSet SelectScanFileName()
        {

            return dao.SelectAllScanFile();
        }

        //找到同一个文件中所有的初始化用户输入
        public DataSet SelectScanSource(string strFileName)
        {

            return dao.SelectFirstSource(strFileName);
        }

         //找到同一个用户输入的所有树节点
        public DataSet SelectAllTree(string strFileName, string strTreeName)
        {
            return dao.SelectAllTreeExceptRoot(strFileName, strTreeName);
        }

        public DataSet SelectAllAST()
        {
            return dao.SelectAllAST();
        }
    }
}
