using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Service.BLL
{
    public class UserBO
    {

        private DAL.UserDao dao;

        public UserBO()
        {

            dao = new DAL.UserDao();
        }

        public bool Select(string strName, string strPwd)
        {
            bool isLogin = false;
            DataSet ds = dao.Select(strName, strPwd);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (strName == ds.Tables[0].Rows[0]["Name"].ToString() && strPwd == ds.Tables[0].Rows[0]["PassWord"].ToString())
                {
                    isLogin = true;
                }
            }

            return isLogin;
        }


        public DataSet GetAllUser()
        {
            return dao.GetAllUser();
        }

        public int InsertUser(Model.User user)
        {

          return  dao.InsertUser(user);
        }


        public int DeleteUser(int userId)
        {
            return dao.DeleteUser(userId);
        }


        public int UpdateUser(Model.User user)
        {

            return dao.UpdateUser(user);
        }


    }
}
