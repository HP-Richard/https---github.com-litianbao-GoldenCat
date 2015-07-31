using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;


namespace JMReports.WebApp.ReportBussiness
{
  public class RoomEfficiencyReport
  {
    public System.Data.DataSet getRoomEfficiency(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.RoomEfficiencyComponent rc = new Business.RoomEfficiencyComponent();
          var ds = rc.getRoomEfficiencyDS(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.RoomEfficiencyComponent rc = new Business.RoomEfficiencyComponent();
        return rc.getRoomEfficiencyDS(HotelId, mYear, mMonth);
      }
    }
  }
}