using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace JMReports.WebApp.ReportBussiness
{
    public class RoomCompeteReport
    {
        public System.Data.DataSet getCombinationDS(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomCompeteComponent  rc = new Business.RoomCompeteComponent();
            return rc.getCombinationDS(HotelId, mYear, mMonth);
        }

        public System.Data.DataSet getRoomCompeteCurrentMonthDS(string HotelId, string mYear, string mMonth, string combination)
        {
            JMReports.Business.RoomCompeteComponent  rc = new Business.RoomCompeteComponent();
            return rc.getRoomCompeteCurrentMonthDS(HotelId, mYear, mMonth, combination);
        }
        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomCompeteComponent  rc = new Business.RoomCompeteComponent();
            return rc.GetRoomCompeteDS(HotelId, mYear, mMonth);
        }

        public System.Data.DataSet getReport2(string HotelId, string mYear, string mMonth, string combination)
        {
            JMReports.Business.RoomCompeteComponent rc = new Business.RoomCompeteComponent();
            return rc.GetRoomCompeteDS2(HotelId, mYear, mMonth, combination);
        }

    }
}