using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class HotelWholeReport
    {
        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.HotelWholeReportComponent hc = new Business.HotelWholeReportComponent();
            return hc.GetHotelWholeReport(HotelId, mYear, mMonth);
        }
    }
}