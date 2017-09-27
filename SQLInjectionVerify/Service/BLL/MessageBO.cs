using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Service.BLL
{
    public class MessageBO
    {
        private DAL.MessageDao dao;

        public MessageBO()
        {
            dao = new DAL.MessageDao();
        }


        public DataSet GetMessageA()
        {
            string strType = "SQL注入研究";
            return dao.SelectMessage(strType);
        }

        public DataSet GetMessageB()
        {
            string strType = "参数化编程";
            return dao.SelectMessage(strType);
        }


        public DataSet GetMessageByCategory(string strCategory)
        {
            return dao.SelectMessage(strCategory);
        }


        public DataSet SelectMessageAll(string msgType)
        {

            return dao.SelectMessageAll(msgType);
        }

        public string GetMessage(int messageId)
        {
            string msg = string.Empty;
            DataSet ds = dao.GetMessage(messageId);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                msg = ds.Tables[0].Rows[0]["Content"].ToString();
            }
            return msg;
        }

        public DataRow GetMessageRow(int messageId)
        {
            string msg = string.Empty;
            DataSet ds = dao.GetMessage(messageId);

            DataRow row = ds.Tables[0].Rows[0]; ;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                row = ds.Tables[0].Rows[0];
            }

            return row;
        }


        public DataSet GetMessages(int messageId)
        {
            string msg = string.Empty;
            return dao.GetMessage(messageId);
        }

        public int UpdateMessage(Model.Message msg)
        {

            return dao.UpdateMessage(msg);
        }

        public int InsertMessage(Model.Message msg)
        {
            return dao.InsertMessage(msg);

        }
        public DataSet Search(string category, string key)
        {

            return dao.Search(category, key);

        }

        public int DeletMessage(int id)
        {

            return dao.DeletMessage(id);
        }

    }
}
