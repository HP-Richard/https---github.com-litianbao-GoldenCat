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
    public partial class ReportInfoDAL : DataAccessComponent
    {


        public List<Report> getReports()
        {
            string SQL_STATEMENT = string.Format(@"
                SELECT ReportId
                      ,Name
                      ,ChineseName
                      ,Description
                      ,Status
                      ,Category
                      ,URL
                  FROM ReportInfo
                  where status=1");



            List<Report> result = new List<Report>();

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
                        Report role = LoadReport(dr);

                        // Add to List.
                        result.Add(role);
                    }
                }
            }

            return result;


        }

        public Report getSingleReportByReportId(int reportId)
        {
            string SQL_STATEMENT = string.Format(@"
                SELECT     ReportId, Name, ChineseName, Description, Status, Category, URL, Sequence
FROM  ReportInfo 
WHERE ReportInfo.Status = 1 AND ReportInfo.ReportId = @reportId");


            Report result = null;
            

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                cmd.CommandText = SQL_STATEMENT;
                db.AddInParameter(cmd, "@reportId", DbType.Int32, reportId);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        // Create a new Account
                        result = LoadReport(dr); 
                    }
                }
            }

            return result;
        }


        public List<Report> getReportsById(int Id)
        {
            string SQL_STATEMENT = string.Format(@"
                SELECT     ReportInfo.ReportId
                      ,ReportInfo.Name
                      ,ReportInfo.ChineseName
                      ,ReportInfo.Description
                      ,ReportInfo.Status
                      ,ReportInfo.Category
                      ,ReportInfo.URL
FROM         SysUser INNER JOIN
                      RoleReport ON SysUser.RoleId = RoleReport.RoleId INNER JOIN
                      ReportInfo ON RoleReport.ReportId = ReportInfo.ReportId INNER JOIN
                      ReportCategory ON ReportCategory.Category = ReportInfo.Category
                      
Where ReportInfo.Status = 1 and SysUser.Id = @Id
Order by ReportCategory.Sequence asc, ReportInfo.Sequence asc");



            List<Report> result = new List<Report>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {

                cmd.CommandText = SQL_STATEMENT;
                db.AddInParameter(cmd, "@Id", DbType.Int32, Id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Account
                        Report role = LoadReport(dr);

                        // Add to List.
                        result.Add(role);
                    }
                }
            }

            return result;


        }


        private Report LoadReport(IDataReader dr)
        {
            // Create a new Role
            Report rep = new Report();

            //Report values
            rep.ReportId = base.GetDataValue<int>(dr, "ReportId");
            rep.Name = base.GetDataValue<string>(dr, "Name");
            rep.ChineseName = base.GetDataValue<string>(dr, "ChineseName");
            rep.Description = base.GetDataValue<string>(dr, "Description");
            rep.Status = base.GetDataValue<int>(dr, "Status");
            rep.Category = base.GetDataValue<string>(dr, "Category");
            rep.URL = base.GetDataValue<string>(dr, "URL");

            return rep;
        }

    }

}
