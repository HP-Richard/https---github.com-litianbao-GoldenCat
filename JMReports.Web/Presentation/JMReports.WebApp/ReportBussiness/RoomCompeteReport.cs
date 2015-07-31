using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;


namespace JMReports.WebApp.ReportBussiness
{
  public class RoomCompeteReport
  {
    public System.Data.DataSet getCombinationDS(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.RoomCompeteComponent rc = new Business.RoomCompeteComponent();
          var ds = rc.getCombinationDS(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.RoomCompeteComponent rc = new Business.RoomCompeteComponent();
        return rc.getCombinationDS(HotelId, mYear, mMonth);
      }
    }

    public System.Data.DataSet getRoomCompeteCurrentMonthDS(string HotelId, string mYear, string mMonth, string combination)
    {
      JMReports.Business.RoomCompeteComponent rc = new Business.RoomCompeteComponent();
      return rc.getRoomCompeteCurrentMonthDS(HotelId, mYear, mMonth, combination);
    }
    public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
    {
      JMReports.Business.RoomCompeteComponent rc = new Business.RoomCompeteComponent();
      return rc.GetRoomCompeteDS(HotelId, mYear, mMonth);
    }

    public System.Data.DataSet getReport2(string HotelId, string mYear, string mMonth, string combination)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth, combination);
        if (cacheds == null)
        {
          JMReports.Business.RoomCompeteComponent rc = new Business.RoomCompeteComponent();
          var ds = rc.GetRoomCompeteDS2(HotelId, mYear, mMonth, combination);
          ds.AddCacheData(HotelId, mYear, mMonth, combination);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth, combination);
      }
      else
      {
        JMReports.Business.RoomCompeteComponent rc = new Business.RoomCompeteComponent();
        return rc.GetRoomCompeteDS2(HotelId, mYear, mMonth, combination);
      }
    }
  }
}