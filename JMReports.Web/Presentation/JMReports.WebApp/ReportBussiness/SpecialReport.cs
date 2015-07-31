using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;

namespace JMReports.WebApp.ReportBussiness
{
  public class SpecialReport
  {

    public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.SpecialReportComponent rc = new Business.SpecialReportComponent();
          var ds = rc.GetSpecialReport(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.SpecialReportComponent rc = new Business.SpecialReportComponent();
        return rc.GetSpecialReport(HotelId, mYear, mMonth);
      }
    }
  }
}