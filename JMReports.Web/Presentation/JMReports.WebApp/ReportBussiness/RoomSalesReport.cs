using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;

namespace JMReports.WebApp.ReportBussiness
{
  public class RoomSalesReport
  {

    public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.RoomSalesReportComponent rc = new Business.RoomSalesReportComponent();
          var ds = rc.GetRoomSalesReport(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.RoomSalesReportComponent rc = new Business.RoomSalesReportComponent();
        return rc.GetRoomSalesReport(HotelId, mYear, mMonth);
      }
    }
  }
}