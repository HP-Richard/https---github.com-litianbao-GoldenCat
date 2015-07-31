using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class DailyReport
    {
        public System.Data.DataSet GetDailyReport(DateTime dt1, string hotel)
        {
            JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
            return dc.GetDailyReport(dt1,hotel);
        }

        public System.Data.DataSet GetDailyRevenueBIReport(DateTime dt1)
        {
            JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
            return dc.getRevenueBI(dt1);
        }


        public System.Data.DataSet GetDailyRoomRevenueBIReport(DateTime dt1)
        {
            JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
            return dc.getRoomRevenueBI(dt1);
        }

        public System.Data.DataSet GetDailyRestaurantRevenueBIReport(DateTime dt1)
        {
            JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
            return dc.getRestaurantRevenueBI(dt1);
        }


        public System.Data.DataSet GetDailyOccupancyRateBIReport(DateTime dt1)
        {
            JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
            return dc.getOccupancyRateBI(dt1);
        }
    }
}