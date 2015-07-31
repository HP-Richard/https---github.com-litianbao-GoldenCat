using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class OperationReport
    {
        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.OperationReportComponent oc = new Business.OperationReportComponent();
            return oc.GetOperationReport(HotelId, mYear, mMonth);
        }
    }
}