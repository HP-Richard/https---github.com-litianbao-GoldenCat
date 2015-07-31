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
    public partial class HotelWholeDAL : DataAccessComponent
    {

        public DataSet getHotelWholeReportDS(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "Item1", "Item2", "MonthActual", "MonthBudget", "ProcessPercent", "LastMonthActual", "QoQ", "LastYear", "YoY", "YearAct", "SameYearBudget", "YearPercent", "QoQYear", "YoYYear", "YearBudget", "YearBudgetPercent", "Orderid", "ItemId" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["MonthActual"].DataType = typeof(decimal);
            dt.Columns["MonthBudget"].DataType = typeof(decimal);
            dt.Columns["ProcessPercent"].DataType = typeof(decimal);
            dt.Columns["LastMonthActual"].DataType = typeof(decimal);
            dt.Columns["QoQ"].DataType = typeof(decimal);
            dt.Columns["LastYear"].DataType = typeof(decimal);
            dt.Columns["YoY"].DataType = typeof(decimal);
            dt.Columns["YearAct"].DataType = typeof(decimal);
            dt.Columns["SameYearBudget"].DataType = typeof(decimal);
            dt.Columns["YearPercent"].DataType = typeof(decimal);
            dt.Columns["QoQYear"].DataType = typeof(decimal);
            dt.Columns["YoYYear"].DataType = typeof(decimal);
            dt.Columns["YearBudget"].DataType = typeof(decimal);
            dt.Columns["YearBudgetPercent"].DataType = typeof(decimal);

            dt.Columns["Orderid"].DataType = typeof(int);
dt.Columns["ItemId"].DataType = typeof(int);

            string SQL_STATEMENT = string.Empty;

                SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[ItemName]
                      ,[AccountType]
                      ,[Department]
                      ,[yearCode]
                      ,[MonthCode]
                      ,[本月实际]
                      ,[本月预算]
                      ,[本月完成比例]
                      ,[上月实际]
                      ,[本月环比增减]
                      ,[上年本月同期]
                      ,[本月同比增减]
                      ,[本年实际]
                      ,[同期预算]
                      ,[本年完成比例]
                      ,[上年本年累计同期]
                      ,[本年同比增减]
                      ,[全年预算]
                      ,[全年完成比例]
                  FROM [Repository].[dbo].[vwHotelWholeReport{0}]
                  where yearCode={1} and monthCode={2}
                    ", mHotelId, mYear, mMonth);
           

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();
                dr1["Item1"] = row["ItemName"].ToString();
                dr1["Item2"] = row["ItemName"].ToString();

                dr1["MonthActual"] = row["本月实际"];
                dr1["MonthBudget"] = row["本月预算"];
                dr1["ProcessPercent"] = row["本月完成比例"];
                dr1["LastMonthActual"] = row["上月实际"];
                dr1["QoQ"] = row["本月环比增减"];
                dr1["LastYear"] = row["上年本月同期"];
                dr1["YoY"] = row["本月同比增减"];
                dr1["YearAct"] = row["本年实际"];
                dr1["SameYearBudget"] = row["同期预算"];
                dr1["YearPercent"] = row["本年完成比例"];
                dr1["QoQYear"] = row["上年本年累计同期"];
                dr1["YoYYear"] = row["本年同比增减"];
                dr1["YearBudget"] = row["全年预算"];
                dr1["YearBudgetPercent"] = row["全年完成比例"];
                dr1["Orderid"] = row["Sequence"];
                dr1["ItemId"] = row["ItemId"];
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }
    }
}
