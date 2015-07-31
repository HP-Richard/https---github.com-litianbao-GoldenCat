using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;

namespace JMReports.WebApp.ReportBussiness
{
  public class BanquetMarketReport
  {

    public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.BanquetMarketReportComponent rc = new Business.BanquetMarketReportComponent();
          var ds = rc.GetBanquetMarketReport(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.BanquetMarketReportComponent rc = new Business.BanquetMarketReportComponent();
        return rc.GetBanquetMarketReport(HotelId, mYear, mMonth);
      }
    }
  }
}