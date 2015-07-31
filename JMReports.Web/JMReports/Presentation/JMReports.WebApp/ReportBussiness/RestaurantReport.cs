using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace JMReports.WebApp.ReportBussiness
{
    public class RestaurantReport
    {

        public System.Data.DataSet getReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
            return rc.GetRestaurant(HotelId, mYear, mMonth);
        }


        //HotelId, mYear, mMonth

        public System.Data.DataSet getRestaurantEfficiency(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
            return rc.getRestaurantEfficiency2(HotelId, mYear, mMonth);
        }


        public System.Data.DataSet GetRestaurantMainReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RestaurantComponent rc = new Business.RestaurantComponent();
            return rc.GetRestaurantMainReport(HotelId, mYear, mMonth);
        }

    }
}