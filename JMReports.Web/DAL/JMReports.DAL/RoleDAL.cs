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
    public partial class RoleDAL : DataAccessComponent
    {


        public List<Role> Select(int maximumRows, int startRowIndex, string sortExpression, string roleName, RoleStatuses? status)
        {
            const string SQL_STATEMENT =
                    "WITH SortedRoles AS " +
                    "(SELECT ROW_NUMBER() OVER (ORDER BY {1}) AS RowNumber, " +
                        "[ID], [RoleName], [Description],[Status],[CreateTime] " +
                        "FROM dbo.Accounts " +
                        "{0}" +
                    ") SELECT * FROM SortedRoles " +
                    "WHERE RowNumber BETWEEN @StartRowIndex AND @EndRowIndex";

            startRowIndex++;
            long endRowIndex = startRowIndex + maximumRows;

            List<Role> result = new List<Role>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Append filters.
                string filter = AppendFilters(db, cmd, roleName, status);

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
                        Role role = LoadRole(dr);

                        // Add to List.
                        result.Add(role);
                    }
                }
            }

            return result;

        }


        public List<Role> getRoles()
        {
            string SQL_STATEMENT = string.Format(@"
            SELECT [Id]
                  ,[RoleName]
                  ,[Description]
                  ,[Status]
                  ,[CreateTime]
              FROM [Role] Where Status=1");


            List<Role> result = new List<Role>();

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
                        Role role = LoadRole(dr);

                        // Add to List.
                        result.Add(role);
                    }
                }
            }

            return result;
        }

        public int Count(string role, RoleStatuses? status)
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
                string filter = AppendFilters(db, cmd, role, status);

                // Construct final WHERE statement. 
                if (!string.IsNullOrWhiteSpace(filter))
                    filter = "WHERE " + base.FormatFilterStatement(filter);

                cmd.CommandText = string.Format(SQL_STATEMENT, filter);

                result = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return result;
        }

        private static string AppendFilters(Database db, DbCommand cmd,
            string rolename, RoleStatuses? status)
        {
            string filter = string.Empty;

            // Employee filter. 
            if (!string.IsNullOrWhiteSpace(rolename))
            {
                db.AddInParameter(cmd, "@rolename", DbType.AnsiString, rolename);
                filter += "AND [RoleName]=@rolename ";
            }

            // Status filter. 
            if (status != null)
            {
                db.AddInParameter(cmd, "@Status", DbType.Byte, status);
                filter += "AND [Status]=@Status ";
            }
            return filter;
        }


        private Role LoadRole(IDataReader dr)
        {
            // Create a new Role
            Role role = new Role();

            // Read values.
            role.Id = base.GetDataValue<int>(dr, "Id");
            role.RoleName = base.GetDataValue<string>(dr, "RoleName");

            role.Description = base.GetDataValue<string>(dr, "Description");
            role.Status  = (RoleStatuses)base.GetDataValue<int>(dr, "Status");

            return role;
        }




        public int insertRoleReport(int RoleId , int Reportid)
        {
            int returnValue = 0;

            const string SQL_STATEMENT =
                "INSERT INTO RoleReport (RoleId,ReportId) " +
                "VALUES(@RoleId, @ReportId) SELECT SCOPE_IDENTITY();";


            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@RoleId", DbType.Int32, RoleId.ToString());
                db.AddInParameter(cmd, "@ReportId", DbType.Int32, Reportid);


                // Get the primary key value.
                returnValue = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return returnValue;

        }

        public int DeleteRoleReport(int RoleId)
        {
            int returnValue = 0;

            const string SQL_STATEMENT =
                "delete RoleReport " +
                "where RoleId=@RoleId";


            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@RoleId", DbType.Int32, RoleId.ToString());
                returnValue = db.ExecuteNonQuery(cmd); 
            }
            return returnValue;
        }

        public string getReportIDs(int RoleId)
        {
            string SQL_STATEMENT = string.Format(@"select ReportId  from dbo.RoleReport where RoleId={0}",RoleId.ToString());

            string reportIds = string.Empty;
            List<Role> result = new List<Role>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                cmd.CommandText = SQL_STATEMENT;

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        reportIds = reportIds + dr["ReportId"].ToString() + ",";
                    }
                }
            }

            if (reportIds != "")
            {
                reportIds = reportIds.Substring(0, reportIds.Length - 1);
            }

            return reportIds;
        }
    }
}
