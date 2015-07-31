using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class OtherBusinessReport
    {
        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.OtherBusinessReportComponent uc = new Business.OtherBusinessReportComponent();
            return uc.GetOtherBusinessReport(HotelId, mYear, mMonth);
        }
    }
}