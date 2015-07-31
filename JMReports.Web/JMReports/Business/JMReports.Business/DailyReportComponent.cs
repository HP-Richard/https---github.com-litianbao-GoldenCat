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
    public partial class DailyReportComponent
    {
        public DataSet GetDailyReport(DateTime dt1, string hotel)
        {

            var DailyReportDAC = new DailyReportDAL() ;

            return DailyReportDAC.getDailyReportDS(dt1,hotel);

        }

        public DataSet getRevenueBI(DateTime dt1)
        {
            var DailyReportDAC = new DailyReportDAL();

            return DailyReportDAC.GetDailyRevenueBIDS(dt1);
        }

        public DataSet getRoomRevenueBI(DateTime dt1)
        {
            var DailyReportDAC = new DailyReportDAL();

            return DailyReportDAC.GetDailyRoomRevenueBIDS(dt1);
        }

        public DataSet getRestaurantRevenueBI(DateTime dt1)
        {
            var DailyReportDAC = new DailyReportDAL();

            return DailyReportDAC.GetDailyRestaurantRevenueBIDS(dt1);
        }

        //平均出租率
        public DataSet getOccupancyRateBI(DateTime dt1)
        {
            var DailyReportDAC = new DailyReportDAL();

            return DailyReportDAC.GetDailyOccupancyRate(dt1);
        }
    }
}
