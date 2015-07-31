using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;

namespace JMReports.WebApp.ReportBussiness
{
  public class OtherBusinessReport
  {
    public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.OtherBusinessReportComponent uc = new Business.OtherBusinessReportComponent();
          var ds = uc.GetOtherBusinessReport(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.OtherBusinessReportComponent uc = new Business.OtherBusinessReportComponent();
        return uc.GetOtherBusinessReport(HotelId, mYear, mMonth);
      }
    }
  }
}