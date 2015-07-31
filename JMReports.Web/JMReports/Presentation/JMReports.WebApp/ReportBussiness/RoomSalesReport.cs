using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class RoomSalesReport
    {

        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomSalesReportComponent rc = new Business.RoomSalesReportComponent();
            return rc.GetRoomSalesReport(HotelId, mYear, mMonth);
        }

    }
}