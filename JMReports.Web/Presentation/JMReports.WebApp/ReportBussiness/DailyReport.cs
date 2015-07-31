using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;

namespace JMReports.WebApp.ReportBussiness
{
  public class DailyReport
  {
    public System.Data.DataSet GetDailyReport(DateTime dt1, string hotel)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(dt1, hotel);
        if (cacheds == null)
        {
          JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
          var ds = dc.GetDailyReport(dt1, hotel);
          ds.AddCacheData(dt1, hotel);
        }
        return Helper.GetCacheData(dt1, hotel);
      }
      else
      {
        JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
        return dc.GetDailyReport(dt1, hotel);
      }
    }

    public System.Data.DataSet GetDailyRevenueBIReport(DateTime dt1)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(dt1);
        if (cacheds == null)
        {
          JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
          var ds = dc.getRevenueBI(dt1);
          ds.AddCacheData(dt1);
        }
        return Helper.GetCacheData(dt1);
      }
      else
      {
        JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
        return dc.getRevenueBI(dt1);
      }
    }


    public System.Data.DataSet GetDailyRoomRevenueBIReport(DateTime dt1)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(dt1);
        if (cacheds == null)
        {
          JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
          var ds = dc.getRoomRevenueBI(dt1);
          ds.AddCacheData(dt1);
        }
        return Helper.GetCacheData(dt1);
      }
      else
      {
        JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
        return dc.getRoomRevenueBI(dt1);
      }
    }

    public System.Data.DataSet GetDailyRestaurantRevenueBIReport(DateTime dt1)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(dt1);
        if (cacheds == null)
        {
          JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
          var ds = dc.getRestaurantRevenueBI(dt1);
          ds.AddCacheData(dt1);
        }
        return Helper.GetCacheData(dt1);
      }
      else
      {
        JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
        return dc.getRestaurantRevenueBI(dt1);
      }
    }


    public System.Data.DataSet GetDailyOccupancyRateBIReport(DateTime dt1)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(dt1);
        if (cacheds == null)
        {
          JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
          var ds = dc.getOccupancyRateBI(dt1);
          ds.AddCacheData(dt1);
        }
        return Helper.GetCacheData(dt1);
      }
      else
      {
        JMReports.Business.DailyReportComponent dc = new Business.DailyReportComponent();
        return dc.getOccupancyRateBI(dt1);
      }
    }
  }
}