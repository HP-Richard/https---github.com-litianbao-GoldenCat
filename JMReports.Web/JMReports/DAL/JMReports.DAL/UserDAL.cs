using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JMReports.Entities;


namespace JMReports.Data
{
    public partial class UserDAL : DataAccessComponent
    {

        public User Create(User user1)
        {
            const string SQL_STATEMENT =
                "INSERT INTO dbo.[SysUser]([UserId],[RoleId],[Title], [Email],[Psd]) " +
                "VALUES(@UserId, @RoleId,@Title, @Email, @Psd); SELECT SCOPE_IDENTITY();";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@UserId", DbType.AnsiString, user1.UserId);
                db.AddInParameter(cmd, "@RoleId", DbType.Int32, user1.RoleId);
                db.AddInParameter(cmd, "@Title", DbType.AnsiString, user1.Title);
                db.AddInParameter(cmd, "@Email", DbType.AnsiString, user1.Email);
                db.AddInParameter(cmd, "@Psd", DbType.AnsiString, user1.Psd);

                // Get the primary key value.
                user1.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return user1;
        }

        public int DeleteUser(string mId)
        {
            string strSql = "delete from SysUser where id in (" + mId + ")";
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            int mReturn = 0;
            mReturn = db.ExecuteNonQuery(CommandType.Text, strSql);

            return mReturn;

        }

        public List<User> getUsers()
        {
           string SQL_STATEMENT = string.Format(@"
                    select u.Id,u.UserId,u.RoleId,r.RoleName,u.Title, u.Email ,u.Psd,u.Status 
                    from SysUser u left join Role r on u.RoleId= r.Id 
                    where u.Status=1");


            List<User> result = new List<User>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                cmd.CommandText = SQL_STATEMENT;

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Account
                        User user1 = new User();
                        user1.Id = int.Parse(dr["Id"].ToString());
                        user1.UserId = dr["userid"].ToString();
                        user1.Title = dr["Title"].ToString();
                        user1.Email = dr["Email"].ToString();
                        user1.RoleId = int.Parse(dr["RoleId"].ToString());
                        user1.RoleName = dr["RoleName"].ToString();
                        user1.Status = (UserStatus)int.Parse(dr["Status"].ToString());

                        // Add to List.
                        result.Add(user1);
                    }
                }
            }

            return result;
        }

        public User GetUserById(int Id)
        {

            User user1 = null;

            string mStrSql = string.Format(@"
                select u.Id,u.UserId,u.RoleId,r.RoleName,u.Title, u.Email ,u.Psd,u.Status,u.CreateTime 
                from SysUser u left join Role r on u.RoleId= r.Id 
                where u.Status=1 and u.id={0}", Id.ToString());

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            using (DbCommand cmd = db.GetSqlStringCommand(mStrSql))
            {
                cmd.CommandText = mStrSql;

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        user1 = new User();
                        user1.Id = int.Parse(dr["Id"].ToString());
                        user1.UserId = dr["userid"].ToString();
                        user1.Title = dr["Title"].ToString();
                        user1.Email = dr["Email"].ToString();
                        user1.RoleId = int.Parse(dr["RoleId"].ToString());
                        user1.RoleName = dr["RoleName"].ToString();
                        user1.Psd = dr["Psd"].ToString();
                        user1.Status = (UserStatus)int.Parse(dr["Status"].ToString());
                        user1.CreateTime = DateTime.Parse(dr["CreateTime"].ToString());
                    }
                }
            }
            return user1;
        }

        public User GetUserByUserId(string mUserId)
        {
            User user1 = null;

            string mStrSql = string.Format(@"
                select u.Id,u.UserId,u.RoleId,r.RoleName,u.Title,u.Email ,u.Psd,u.Status 
                from SysUser u left join Role r on u.RoleId= r.Id 
                where u.Status=1 and u.UserId='{0}'", mUserId.ToString());

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            using (DbCommand cmd = db.GetSqlStringCommand(mStrSql))
            {

                cmd.CommandText = mStrSql;

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Account
                        //user = LoadUser(dr);
                        user1 = new User();
                        user1.Id = int.Parse(dr["Id"].ToString());
                        user1.UserId = dr["userid"].ToString();
                        user1.Title = dr["Title"].ToString();
                        user1.Email = dr["Email"].ToString();
                        user1.RoleId = int.Parse(dr["RoleId"].ToString());
                        user1.RoleName = dr["RoleName"].ToString();
                        user1.Status = (UserStatus)int.Parse(dr["Status"].ToString());
                    }
                }
            }

            return user1;
        }

        public bool VerifyPassword(string UserID, string Password)
        {
            string selSql = string.Format(@"select count(*)
                    from SysUser
                    where UserId = '{0}' and Psd='{1}'", UserID,Password);

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            int recordNum = int.Parse(db.ExecuteScalar(CommandType.Text, selSql).ToString());

            if (recordNum == 1)
            {
               return true;
            }
            else
            {
                return false;
            }
        }

        public int Update(User user1)
        {

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            string strSql = string.Empty;

            int i = 0;

            if (user1.Psd != "")
            {
                strSql = string.Format(@"update SysUser set UserId='{0}',RoleId={1},Title='{2}',Email='{3}',Psd='{4}' where id={5}", user1.UserId, user1.RoleId, user1.Title, user1.Email, user1.Psd, user1.Id);

            }
            else
            {
                strSql = string.Format(@"update SysUser set UserId='{0}',RoleId={1},Title='{2}',Email='{3}' where id={4}", user1.UserId, user1.RoleId, user1.Title, user1.Email, user1.Id);
            }

            i = db.ExecuteNonQuery(CommandType.Text ,strSql); 

            return i;
        }

    }
}
