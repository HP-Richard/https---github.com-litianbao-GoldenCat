using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JMReports.Entities;
using JMReports.Data;
using System.Transactions;
using System.Linq;
using System.Data;

namespace JMReports.Business
{
    public partial class RestaurantComponent
    {
        public DataSet GetRestaurantVar(DateTime dt1,string hotel, string restaurant,string mYear,string mMonth)
        {
            var RestaurantDAC = new RestaurantReportDAL();

            return RestaurantDAC.getRestaurantVarDS(dt1,hotel,restaurant,mYear,mMonth); 
        }


        public DataSet GetRestaurantEfficiency(string HotelId, string mYear, string mMonth)
        {

            RestaurantReportDAL RestaurantDAC = new RestaurantReportDAL();

            return RestaurantDAC.getRestaurantEfficiency(HotelId, mYear, mMonth);
        }

        public DataSet GetRestaurant(string HotelId, string mYear, string mMonth)
        {
            RestaurantReportDAL rr = new RestaurantReportDAL();
            return rr.getRestaurantReportDS(HotelId, mYear, mMonth);
 
        }

        public DataSet GetRestaurantMainReport(string HotelId, string mYear, string mMonth)
        {
            RestaurantReportDAL rr = new RestaurantReportDAL();
            return  rr.getRestaurantMainReportDS(HotelId,mYear,mMonth);
        }

        public DataSet getRestaurantEfficiency2(string HotelId, string mYear, string mMonth)
        {
            RestaurantReportDAL RestaurantDAC = new RestaurantReportDAL();

            return RestaurantDAC.getRestaurantEfficiency2(HotelId, mYear, mMonth);

        }

    }
}
