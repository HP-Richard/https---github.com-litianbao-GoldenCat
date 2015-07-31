using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using JMReports.Entities;
using JMReports.Data;

namespace JMReports.Business
{

    [Serializable()]
    public class MyIdentity : System.Security.Principal.IIdentity
    {
        private  User user;

        #region 属性

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }


        public int Id
        {
            get
            {
                return user.Id ;
            }
        }


        public string Name
        {
            get
            {
                return user.UserId;
            }
        }

        public string Psd
        {
            get
            {
                return user.Psd.ToString();
            }
        }


        public string AuthenticationType
        {
            get
            {
                return "自定义身份验证";
            }

        }


        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }


        #endregion


        #region 方法

        public MyIdentity()
        {
            user = null;
        }


        public MyIdentity(int Id)
        {


            UserDAL userDAC = new UserDAL();

            user = userDAC.GetUserById(Id);
        }


        public MyIdentity(string UserId)
        {
            UserDAL userDAC = new UserDAL();

            user = userDAC.GetUserByUserId(UserId);
        }


        public bool checkPassword(string psd)
        {
            if (psd != user.Psd.ToString().Trim())
            {
                return false;
            }

            return true;
        }
        #endregion








    }
}
