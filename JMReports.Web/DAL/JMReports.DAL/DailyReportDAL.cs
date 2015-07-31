using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JMReports.Entities;

namespace JMReports.Data
{
    public partial class DailyReportDAL : DataAccessComponent
    {
        string hotelId = "1";

        public DataSet getDailyReportDS(DateTime dt1, string hotel)
        {

            hotelId = hotel;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "id", "Hotel", "Today", "Item", "IncomeToday", "AccumulativeTotal", "BudgetThisMonth", "AccumulativeBudgetProgress", "IncomeLastYearToday", "IncreasedYearOnYear", "AccumulativeIncomeThisYear", "AccumulativeBudgetThisYear", "AccumulativeBudgetProgressThisYear", "AccumulativeIncomeLastYear", "IncreasedByMovingAnnualTotal" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["id"].DataType = typeof(int);
            dt.Columns["IncomeToday"].DataType = typeof(decimal);
            dt.Columns["AccumulativeTotal"].DataType = typeof(decimal);
            dt.Columns["BudgetThisMonth"].DataType = typeof(decimal);
            dt.Columns["AccumulativeBudgetProgress"].DataType = typeof(decimal);
            dt.Columns["IncomeLastYearToday"].DataType = typeof(decimal);
            dt.Columns["IncreasedYearOnYear"].DataType = typeof(decimal);
            dt.Columns["AccumulativeIncomeThisYear"].DataType = typeof(decimal);
            dt.Columns["AccumulativeBudgetThisYear"].DataType = typeof(decimal);
            dt.Columns["AccumulativeBudgetProgressThisYear"].DataType = typeof(decimal);
            dt.Columns["AccumulativeIncomeLastYear"].DataType = typeof(decimal);
            dt.Columns["IncreasedByMovingAnnualTotal"].DataType = typeof(decimal);


            string SQL_STATEMENT = string.Empty;

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                          ,AccumulativeTotal
                          ,BudgetThisMonth
                          ,AccumulativeBudgetProgress
                          ,IncomeLastYearToday
                          ,IncreasedYearOnYear
                          ,AccumulativeIncomeLastYear
                          ,IncreasedByMovingAnnualTotal
                      FROM vwDailySummaryReport{0}
                      where HotelId='{0}' and Today='{1}'
                      order by id
                    ", hotel, dt1.ToString("yyyy-MM-dd"));

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds1.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["id"] = item["id"];
                dr1["Hotel"] = item["Hotel"];
                dr1["Today"] = item["Today"];
                dr1["Item"] = item["Item"];
                dr1["IncomeToday"] = item["IncomeToday"];
                dr1["AccumulativeTotal"] = item["AccumulativeTotal"];
                dr1["BudgetThisMonth"] = item["BudgetThisMonth"];
                dr1["AccumulativeBudgetProgress"] = item["AccumulativeBudgetProgress"];
                dr1["IncomeLastYearToday"] = item["IncomeLastYearToday"];
                dr1["IncreasedYearOnYear"] = item["IncreasedYearOnYear"];
                dr1["AccumulativeIncomeLastYear"] = item["AccumulativeIncomeLastYear"];
                dr1["IncreasedByMovingAnnualTotal"] = item["IncreasedByMovingAnnualTotal"];

                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }


        public DataSet GetDailyRevenueBIDS(DateTime dt1)
        {
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "HotelName", "Revenue", "OnDate" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(string);
            dt.Columns["Revenue"].DataType = typeof(decimal);

            string SQL_STATEMENT = string.Empty;

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport1
                      where HotelId='{0}' and Today='{1}' and item='总收入(万元)'
                      order by id
                    ", "1", dt1.ToString("yyyy-MM-dd"));

            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds1.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "1";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport2
                      where HotelId='{0}' and Today='{1}' and item='总收入(万元)'
                      order by id
                    ", "2", dt1.ToString("yyyy-MM-dd"));

            DataSet ds2 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds2.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "2";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,MockName1 Item
                          ,IncomeToday
                      FROM vwDailySummaryReport3
                      where HotelId='{0}' and Today='{1}' and MockName1='总收入(万元)'
                      order by id
                    ", "3", dt1.ToString("yyyy-MM-dd"));

            DataSet ds3 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds3.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "3";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }


            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,MockName1 Item
                          ,IncomeToday
                      FROM vwDailySummaryReport3
                      where HotelId='{0}' and Today='{1}' and MockName1='总收入(万元)'
                      order by id
                    ", "4", dt1.ToString("yyyy-MM-dd"));

            DataSet ds4 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds4.Tables[0].Rows)
            {
                DataRow dr4 = dt.NewRow();
                dr4["HotelId"] = "4";
                dr4["HotelName"] = item["Hotel"];
                dr4["OnDate"] = item["Today"];
                dr4["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr4);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        public DataSet GetDailyRoomRevenueBIDS(DateTime dt1)
        {
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "HotelName", "Revenue", "OnDate" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(string);
            dt.Columns["Revenue"].DataType = typeof(decimal);

            string SQL_STATEMENT = string.Empty;

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport1
                      where HotelId='{0}' and Today='{1}' and item='客房收入(万元)'
                      order by id
                    ", "1", dt1.ToString("yyyy-MM-dd"));

            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds1.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "1";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport2
                      where HotelId='{0}' and Today='{1}' and item='客房收入(万元)'
                      order by id
                    ", "2", dt1.ToString("yyyy-MM-dd"));

            DataSet ds2 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds2.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "2";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,MockName1 Item
                          ,IncomeToday
                      FROM vwDailySummaryReport3
                      where HotelId='{0}' and Today='{1}' and MockName1='客房收入(万元)'
                      order by id
                    ", "3", dt1.ToString("yyyy-MM-dd"));

            DataSet ds3 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds3.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "3";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,MockName1 Item
                          ,IncomeToday
                      FROM vwDailySummaryReport3
                      where HotelId='{0}' and Today='{1}' and MockName1='客房收入(万元)'
                      order by id
                    ", "4", dt1.ToString("yyyy-MM-dd"));

            DataSet ds4 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds4.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "4";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        public DataSet GetDailyRestaurantRevenueBIDS(DateTime dt1)
        {
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "HotelName", "Revenue", "OnDate" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(string);
            dt.Columns["Revenue"].DataType = typeof(decimal);

            string SQL_STATEMENT = string.Empty;

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport1
                      where HotelId='{0}' and Today='{1}' and item='餐饮收入(万元)'
                      order by id
                    ", "1", dt1.ToString("yyyy-MM-dd"));

            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds1.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "1";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }


            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport2
                      where HotelId='{0}' and Today='{1}' and item='餐饮收入(万元)'
                      order by id
                    ", "2", dt1.ToString("yyyy-MM-dd"));

            DataSet ds2 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds2.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "2";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,MockName1 Item
                          ,IncomeToday
                      FROM vwDailySummaryReport3
                      where HotelId='{0}' and Today='{1}' and MockName1='餐饮收入(万元)'
                      order by id
                    ", "3", dt1.ToString("yyyy-MM-dd"));

            DataSet ds3 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds3.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "3";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,MockName1 Item
                          ,IncomeToday
                      FROM vwDailySummaryReport3
                      where HotelId='{0}' and Today='{1}' and MockName1='餐饮收入(万元)'
                      order by id
                    ", "4", dt1.ToString("yyyy-MM-dd"));

            DataSet ds4 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds4.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "4";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        public DataSet GetDailyOccupancyRate(DateTime dt1)
        {
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "HotelName", "Revenue", "OnDate" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(string);
            dt.Columns["Revenue"].DataType = typeof(decimal);

            string SQL_STATEMENT = string.Empty;

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport1
                      where HotelId='{0}' and Today='{1}' and item='平均出租率'
                      order by id
                    ", "1", dt1.ToString("yyyy-MM-dd"));

            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds1.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "1";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport2
                      where HotelId='{0}' and Today='{1}' and item='平均出租率'
                      order by id
                    ", "2", dt1.ToString("yyyy-MM-dd"));

            DataSet ds2 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds2.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "2";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                //dr1["Revenue"] = 0.43;
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,MockName1 Item
                          ,IncomeToday
                      FROM vwDailySummaryReport3
                      where HotelId='{0}' and Today='{1}' and MockName1='平均出租率'
                      order by id
                    ", "3", dt1.ToString("yyyy-MM-dd"));

            DataSet ds3 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds3.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "3";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                //dr1["Revenue"] = 0.43;
                dt.Rows.Add(dr1);
            }

            SQL_STATEMENT = string.Format(@"
                    SELECT id
                          ,Hotel
                          ,Today
                          ,Item
                          ,IncomeToday
                      FROM vwDailySummaryReport2
                      where HotelId='{0}' and Today='{1}' and item='平均出租率'
                      order by id
                    ", "4", dt1.ToString("yyyy-MM-dd"));

            DataSet ds4 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            foreach (DataRow item in ds4.Tables[0].Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["HotelId"] = "4";
                dr1["HotelName"] = item["Hotel"];
                dr1["OnDate"] = item["Today"];
                dr1["Revenue"] = item["IncomeToday"];
                //dr1["Revenue"] = 0.43;
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);

            return ds;
        }
    }
}
