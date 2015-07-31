using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class UnallocateReport
    {
        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.UnallocateReportComponent uc = new Business.UnallocateReportComponent();
            return uc.GetUnallocateReport(HotelId, mYear, mMonth);
        }
    }
}