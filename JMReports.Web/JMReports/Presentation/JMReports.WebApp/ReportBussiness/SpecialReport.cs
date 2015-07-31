using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class SpecialReport
    {

        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.SpecialReportComponent rc = new Business.SpecialReportComponent();
            return rc.GetSpecialReport(HotelId, mYear, mMonth);
        }
    }
}