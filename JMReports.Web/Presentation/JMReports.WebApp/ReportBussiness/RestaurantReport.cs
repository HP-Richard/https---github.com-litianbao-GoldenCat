using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;


namespace JMReports.WebApp.ReportBussiness
{
  public class RestaurantReport
  {

    public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
          var ds = rc.GetRestaurant(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
        return rc.GetRestaurant(HotelId, mYear, mMonth);
      }
    }


    //HotelId, mYear, mMonth

    public System.Data.DataSet getRestaurantEfficiency(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
          var ds = rc.getRestaurantEfficiency2(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
        return rc.getRestaurantEfficiency2(HotelId, mYear, mMonth);
      }
    }


    public System.Data.DataSet GetRestaurantMainReport(string HotelId, string mYear, string mMonth)
    {
      if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
      {
        var cacheds = Helper.GetCacheData(HotelId, mYear, mMonth);
        if (cacheds == null)
        {
          JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
          var ds = rc.GetRestaurantMainReport(HotelId, mYear, mMonth);
          ds.AddCacheData(HotelId, mYear, mMonth);
        }
        return Helper.GetCacheData(HotelId, mYear, mMonth);
      }
      else
      {
        JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
        return rc.GetRestaurantMainReport(HotelId, mYear, mMonth);
      }
    }

  }
}