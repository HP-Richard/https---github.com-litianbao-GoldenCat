using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JMReports.Entities;
using JMReports.Data;
using System.Transactions;
using System.Linq;
using System.Data;

namespace JMReports.Business
{
    public partial class ReportInfoComponent
    {

        public List<Role> getRoles()
        {
            List<Role> result = default(List<Role>);

            // Data access component declarations.
            var RoleDAC = new RoleDAL();

            result = RoleDAC.getRoles();
            return result;

        }

        public List<Report> getReports()
        {
            List<Report> result = new List<Report>();
            var ReportDAC = new ReportInfoDAL();

            result = ReportDAC.getReports();
            return result;
        }

        public Report getSingleReportByReportId(int reportId)
        {
            var ReportDAC = new ReportInfoDAL();

            var result = ReportDAC.getSingleReportByReportId(reportId);
            return result;
        }

        public List<Report> getReportsById(int Id)
        {
            List<Report> result = new List<Report>();
            var ReportDAC = new ReportInfoDAL();

            result = ReportDAC.getReportsById(Id);
            return result;
        }


    }
}
