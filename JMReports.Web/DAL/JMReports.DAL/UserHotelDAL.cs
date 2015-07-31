using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

using JMReports.Entities;



namespace JMReports.Data
{
    public class UserHotelDAL : DataAccessComponent
    {
        public List<Hotel> getUserHotel (int userId)
        {
            string SQL_STATEMENT = string.Format(@"
                    SELECT     Hotel.Code, Hotel.Name, Hotel.ChineseName, Hotel.HotelId
                    FROM         UserHotel INNER JOIN Hotel ON UserHotel.HotelId = Hotel.HotelId
                    WHERE UserHotel.UserId = @UserId");


            List<Hotel> result = new List<Hotel>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                cmd.CommandText = SQL_STATEMENT;
                db.AddInParameter(cmd, "@UserId", DbType.Int32, userId.ToString());

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Account
                        Hotel hotel1 = new Hotel();
                        hotel1.HotelId = int.Parse(dr["HotelId"].ToString());
                        hotel1.Code = dr["Code"].ToString();
                        hotel1.Name = dr["Name"].ToString();
                        hotel1.ChineseName = dr["ChineseName"].ToString();

                        // Add to List.
                        result.Add(hotel1);
                    }
                }
            }

            return result;
        }

        public int deleteUserHotel(int userId)
        {
            int returnValue = 0;

            const string SQL_STATEMENT =
                "delete UserHotel " +
                "where UserId=@UserId";


            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@UserId", DbType.Int32, userId.ToString());
                returnValue = db.ExecuteNonQuery(cmd);
            }
            return returnValue;
        }

        public int insertUserHotel(int userId, int hotelId)
        {
            int returnValue = 0;

            const string SQL_STATEMENT =
                "INSERT INTO UserHotel (UserId,HotelId) " +
                "VALUES(@UserId, @HotelId);";


            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@UserId", DbType.Int32, userId.ToString());
                db.AddInParameter(cmd, "@HotelId", DbType.Int32, hotelId);


                // Get the return value.
                returnValue = db.ExecuteNonQuery(cmd);
            }
            
            return returnValue;
        }
    }
}
