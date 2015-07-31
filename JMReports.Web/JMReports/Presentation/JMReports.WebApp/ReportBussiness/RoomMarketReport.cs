﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class RoomMarketReport
    {

        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="HotelId"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <returns></returns>
        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomMarketReportComponent rc = new Business.RoomMarketReportComponent();
            return rc.GetRoomMarketReport(HotelId, mYear, mMonth);
        }

        public System.Data.DataSet getReport2(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomMarketReportComponent rc = new Business.RoomMarketReportComponent();
            return rc.GetRoomMarketReport2(HotelId, mYear, mMonth);
        }

        public System.Data.DataSet getRoomMarketCompanyPrice(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomMarketReportComponent rc = new Business.RoomMarketReportComponent();
            return rc.GetRoomMarketCompanyPriceDS(HotelId, mYear, mMonth);
        }

        public System.Data.DataSet getRoomMarketGroupDetail(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomMarketReportComponent rc = new Business.RoomMarketReportComponent();
            return rc.GetRoomMarketGroupDetailDS(HotelId, mYear, mMonth);
        }

    }
}