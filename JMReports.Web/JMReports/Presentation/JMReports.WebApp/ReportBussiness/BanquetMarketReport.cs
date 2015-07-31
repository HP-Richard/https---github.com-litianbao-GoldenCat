using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class BanquetMarketReport
    {

        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.BanquetMarketReportComponent rc = new Business.BanquetMarketReportComponent();
            return rc.GetBanquetMarketReport(HotelId, mYear, mMonth);
        }


    }
}