using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JMReports.Entities;
using JMReports.Data;
using System.Transactions;
using System.Linq;

namespace JMReports.Business
{
    public partial class UserComponent
    {

        public User Create(User user)
        {
            var UserDAC = new UserDAL();
            return UserDAC.Create(user); 
        }

        public List<User> getUsers()
        {
            var UserDAC = new UserDAL();

            return UserDAC.getUsers(); 
        }


        public bool VerifyPassword(string UserID, string Password)
        {
            var UserDAC = new UserDAL();
            return UserDAC.VerifyPassword(UserID, Password); 
        }

        public int DeleteUser(string mId)
        {
            var UserDAC = new UserDAL();
            return UserDAC.DeleteUser(mId); 
        }

        public User getUserById(string mId)
        {
            var userDAC = new UserDAL();

            return userDAC.GetUserById(int.Parse(mId));
 
        }

        public int Update(User user1)
        {
            var UserDAC = new UserDAL();

            return UserDAC.Update(user1); 
        }
    }
}
