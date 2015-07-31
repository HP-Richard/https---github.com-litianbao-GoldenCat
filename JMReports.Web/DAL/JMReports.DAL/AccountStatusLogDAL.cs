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
    /// <summary>
    /// AccountStatusLogs data access component. Manages CRUD operations for the AccountStatusLogs table.
    /// </summary>
    public partial class AccountStatusLogDAL : DataAccessComponent
    {

        /// <summary>
        /// Inserts a new row in the AccountStatusLogs table.
        /// </summary>
        /// <param name="AccountStatusLog">A AccountStatusLog object.</param>
        /// <returns>An updated AccountStatusLog object.</returns>
        public AccountStatusLog Create(AccountStatusLog AccountStatusLog)
        {
            const string SQL_STATEMENT =
                "INSERT INTO dbo.AccountStatusLogs ([AccountID], [Status], [Date]) " +
                "VALUES(@AccountID, @Status, @Date); SELECT SCOPE_IDENTITY();";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@AccountID", DbType.Int64, AccountStatusLog.AccountID);
                db.AddInParameter(cmd, "@Status", DbType.Byte, AccountStatusLog.Status);
                db.AddInParameter(cmd, "@Date", DbType.DateTime, AccountStatusLog.Date);

                // Get the primary key value.
                AccountStatusLog.LogID = Convert.ToInt64(db.ExecuteScalar(cmd));
            }

            return AccountStatusLog;
        }

        /// <summary>
        /// Conditionally retrieves one or more rows from the AccountStatusLogs table.
        /// </summary>
        /// <param name="AccountID">A AccountID value.</param>
        /// <returns>A collection of AccountStatusLog objects.</returns>		
        public List<AccountStatusLog> SelectByAccount(long AccountID)
        {
            const string SQL_STATEMENT =
                "SELECT [LogID], [AccountID], [Status], [Date] " +
                "FROM dbo.AccountStatusLogs " +
                "WHERE [AccountID]=@AccountID " +
                "ORDER BY [Date] DESC";

            List<AccountStatusLog> result = new List<AccountStatusLog>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@AccountID", DbType.Int64, AccountID);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new AccountStatusLog
                        AccountStatusLog AccountStatusLog = new AccountStatusLog();

                        // Read values.
                        AccountStatusLog.LogID = base.GetDataValue<long>(dr, "LogID");
                        AccountStatusLog.AccountID = base.GetDataValue<long>(dr, "AccountID");
                        AccountStatusLog.Status = base.GetDataValue<AccountStatuses>(dr, "Status");
                        AccountStatusLog.Date = base.GetDataValue<DateTime>(dr, "Date");

                        // Add to List.
                        result.Add(AccountStatusLog);
                    }
                }
            }

            return result;
        }
    }
}
