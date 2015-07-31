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
    public partial class ReportItemComponent
    {
        public DataTable getReportItems()
        {
            // Data access component declarations.
            var ReportItemDAC = new ReportItemDAL();

            DataTable result = ReportItemDAC.getReportItems();
            return result;

        }

        public DataTable getAnalysisItems()
        {
            var AnalysisItemDAC = new ReportItemDAL();

            DataTable result = AnalysisItemDAC.getAnalysisItems();
            return result;
        }

    }
}
