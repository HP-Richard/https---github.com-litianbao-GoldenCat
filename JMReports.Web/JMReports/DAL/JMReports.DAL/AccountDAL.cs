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
    /// Accounts data access component. Manages CRUD operations for the Accounts table.
    /// </summary>
    public partial class AccountDAL : DataAccessComponent
    {
        /// <summary>
        /// Inserts a new row in the Accounts table.
        /// </summary>
        /// <param name="Account">A Account object.</param>
        /// <returns>An updated Account object.</returns>
        public Account Create(Account Account)
        {
            const string SQL_STATEMENT =
                "INSERT INTO dbo.Accounts ([CorrelationID], [Category], [Employee], [StartDate], [EndDate], [Description], [Duration], [Status], [IsCompleted], [Remarks], [DateSubmitted]) " +
                "VALUES(@CorrelationID, @Category, @Employee, @StartDate, @EndDate, @Description, @Duration, @Status, @IsCompleted, @Remarks, @DateSubmitted); SELECT SCOPE_IDENTITY();";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@CorrelationID", DbType.Guid, Account.CorrelationID);
                db.AddInParameter(cmd, "@Category", DbType.Byte, Account.Category);
                db.AddInParameter(cmd, "@Employee", DbType.AnsiString, Account.Employee);
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime, Account.StartDate);
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime, Account.EndDate);
                db.AddInParameter(cmd, "@Description", DbType.AnsiString, Account.Description);
                db.AddInParameter(cmd, "@Duration", DbType.Byte, Account.Duration);
                db.AddInParameter(cmd, "@Status", DbType.Byte, Account.Status);
                db.AddInParameter(cmd, "@IsCompleted", DbType.Boolean, Account.IsCompleted);
                db.AddInParameter(cmd, "@Remarks", DbType.AnsiString, Account.Remarks);
                db.AddInParameter(cmd, "@DateSubmitted", DbType.DateTime, Account.DateSubmitted);

                // Get the primary key value.
                Account.AccountID = Convert.ToInt64(db.ExecuteScalar(cmd));
            }

            return Account;
        }

        /// <summary>
        /// Updates an existing row in the Accounts table.
        /// </summary>
        /// <param name="Account">A Account entity object.</param>
        public void UpdateStatus(Account Account)
        {
            const string SQL_STATEMENT =
                "UPDATE dbo.Accounts " +
                "SET " +
                    "[Status]=@Status, " +
                    "[IsCompleted]=@IsCompleted, " +
                    "[Remarks]=@Remarks " +
                "WHERE [AccountID]=@AccountID ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@Status", DbType.Byte, Account.Status);
                db.AddInParameter(cmd, "@IsCompleted", DbType.Boolean, Account.IsCompleted);
                db.AddInParameter(cmd, "@Remarks", DbType.AnsiString, Account.Remarks);
                db.AddInParameter(cmd, "@AccountID", DbType.Int64, Account.AccountID);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Returns a row from the Accounts table.
        /// </summary>
        /// <param name="AccountID">A AccountID value.</param>
        /// <returns>A Account object with data populated from the database.</returns>
        public Account SelectById(long AccountID)
        {
            const string SQL_STATEMENT =
                "SELECT [AccountID], [CorrelationID], [Category], [Employee], [StartDate], [EndDate], [Description]" +
                        ", [Duration], [Status], [IsCompleted], [Remarks], [DateSubmitted] " +
                "FROM dbo.Accounts  " +
                "WHERE [AccountID]=@AccountID ";

            Account Account = null;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@AccountID", DbType.Int64, AccountID);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        // Create a new Account
                        Account = LoadAccount(dr);
                    }
                }
            }

            return Account;
        }

        /// <summary>
        /// Conditionally retrieves one or more rows from the Accounts table with paging and a sort expression.
        /// </summary>
        /// <param name="maximumRows">The maximum number of rows to return.</param>
        /// <param name="startRowIndex">The starting row index.</param>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="employee">A employee value.</param>
        /// <param name="category">A category value.</param>
        /// <param name="status">A status value.</param>
        /// <returns>A collection of Account objects.</returns>		
        public List<Account> Select(int maximumRows, int startRowIndex, string sortExpression,
            string employee, AccountCategories? category, AccountStatuses? status)
        {
            const string SQL_STATEMENT =
                "WITH SortedAccounts AS " +
                "(SELECT ROW_NUMBER() OVER (ORDER BY {1}) AS RowNumber, " +
                    "[AccountID], [CorrelationID], [Category], [Employee], [StartDate], [EndDate], [Description]" +
                        ", [Duration], [Status], [IsCompleted], [Remarks], [DateSubmitted] " +
                    "FROM dbo.Accounts " +
                    "{0}" +
                ") SELECT * FROM SortedAccounts " +
                "WHERE RowNumber BETWEEN @StartRowIndex AND @EndRowIndex";

            startRowIndex++;
            long endRowIndex = startRowIndex + maximumRows;

            List<Account> result = new List<Account>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Append filters.
                string filter = AppendFilters(db, cmd, employee, category, status);

                // Construct final WHERE statement. 
                if (!string.IsNullOrWhiteSpace(filter))
                    filter = "WHERE " + base.FormatFilterStatement(filter);

                cmd.CommandText = string.Format(SQL_STATEMENT, filter, sortExpression);

                // Paging Parameters.
                db.AddInParameter(cmd, "@StartRowIndex", DbType.Int64, startRowIndex);
                db.AddInParameter(cmd, "@EndRowIndex", DbType.Int64, endRowIndex);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Account
                        Account Account = LoadAccount(dr);

                        // Add to List.
                        result.Add(Account);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Conditionally appends filters to the query statememt.
        /// </summary>
        /// <param name="db">A Database object.</param>
        /// <param name="cmd">A DbCommand object.</param>
        /// <param name="employee">The employee to filter by.</param>
        /// <param name="category">The category to filter by.</param>
        /// <param name="status">The status to filter by.</param>
        /// <returns>A condition statement.</returns>
        private static string AppendFilters(Database db, DbCommand cmd, 
            string employee, AccountCategories? category, AccountStatuses? status)
        {
            string filter = string.Empty;

            // Employee filter. 
            if (!string.IsNullOrWhiteSpace(employee))
            {
                db.AddInParameter(cmd, "@Employee", DbType.AnsiString, employee);
                filter += "AND [Employee]=@Employee ";
            }

            // Category filter. 
            if (category != null)
            {
                db.AddInParameter(cmd, "@Category", DbType.Byte, category);
                filter += "AND [Category]=@Category ";
            }

            // Status filter. 
            if (status != null)
            {
                db.AddInParameter(cmd, "@Status", DbType.Byte, status);
                filter += "AND [Status]=@Status ";
            }
            return filter;
        }

        /// <summary>
        /// Returns a count based on the condition.
        /// </summary>
        /// <param name="employee">A employee value.</param>
        /// <param name="category">A category value.</param>
        /// <param name="status">A status value.</param>
        /// <returns>The record count.</returns>		
        public int Count(string employee, AccountCategories? category, AccountStatuses? status)
        {
            const string SQL_STATEMENT =
                "SELECT COUNT(1) " +
                "FROM dbo.Accounts " +
                "{0}";

            int result = 0;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Append filters.
                string filter = AppendFilters(db, cmd, employee, category, status);

                // Construct final WHERE statement. 
                if (!string.IsNullOrWhiteSpace(filter))
                    filter = "WHERE " + base.FormatFilterStatement(filter);

                cmd.CommandText = string.Format(SQL_STATEMENT, filter); 
                
                result = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return result;
        }

        /// <summary>
        /// Indicates whether there are any overlapping dates from other Accounts.
        /// </summary>
        /// <param name="Account">A Account object.</param>
        /// <returns>Returns true if there is an overlap, false if there is not.</returns>
        public bool IsOverlap(Account Account)
        {
            const string SQL_STATEMENT =
                "SELECT AccountID " +
                  "FROM Accounts " +
                 "WHERE Employee=@Employee " +
                   "AND StartDate <= @EndDate AND EndDate >= @StartDate " +
                   "AND ([Status]=0 OR [Status]=2) ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Employee", DbType.AnsiString, Account.Employee);
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime, Account.StartDate);
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime, Account.EndDate);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    return dr.Read();
                }
            }
        }

        /// <summary>
        /// Creates a new Account from a Datareader.
        /// </summary>
        /// <param name="dr">A DataReader object.</param>
        /// <returns>Returns a Account.</returns>		
        private Account LoadAccount(IDataReader dr)
        {
            // Create a new Account
            Account Account = new Account();

            // Read values.
            Account.AccountID = base.GetDataValue<long>(dr, "AccountID");
            Account.CorrelationID = base.GetDataValue<Guid>(dr, "CorrelationID");
            Account.Category = base.GetDataValue<AccountCategories>(dr, "Category");
            Account.Employee = base.GetDataValue<string>(dr, "Employee");
            Account.StartDate = base.GetDataValue<DateTime>(dr, "StartDate");
            Account.EndDate = base.GetDataValue<DateTime>(dr, "EndDate");
            Account.Description = base.GetDataValue<string>(dr, "Description");
            Account.Duration = base.GetDataValue<byte>(dr, "Duration");
            Account.Status = base.GetDataValue<AccountStatuses>(dr, "Status");
            Account.IsCompleted = base.GetDataValue<bool>(dr, "IsCompleted");
            Account.Remarks = base.GetDataValue<string>(dr, "Remarks");
            Account.DateSubmitted = base.GetDataValue<DateTime>(dr, "DateSubmitted");

            return Account;
        }


    }
}

