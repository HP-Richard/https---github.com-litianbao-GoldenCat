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
    public partial class RoomMarketReportDAL : DataAccessComponent
    {
        public DataSet getRoomMarketReportDS(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "Item1", "Item2", "MonthActual", "MonthBudget", "ProcessPercent", "LastMonthActual", "QoQ", "LastYear", "YoY", "YearAct", "SameYearBudget", "YearPercent", "QoQYear", "YoYYear", "YearBudget", "YearBudgetPercent", "Orderid" };
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

            string SQL_STATEMENT = string.Format(@"
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
                  FROM [Repository].[dbo].[vwRoomMarketReport]
                  where yearCode={0} and monthCode={1}
                    ", mYear, mMonth);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();
                dr1["Item1"] = row["AccountType"].ToString();
                dr1["Item2"] = row["Department"].ToString();

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
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet getRoomMarketReportDS2(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] {"HotelId","ItemId","ItemEName","ItemCName","KPI","MonthActual","MonthBudget","ProcessPercent","LastMonthActual","QoQ","YearActual","YearBudgetActual","YearPercent","QoQYear","YoYYear","OnYear","OnMonth" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(int);
            dt.Columns["ItemId"].DataType = typeof(int);

            dt.Columns["MonthActual"].DataType = typeof(decimal);
            dt.Columns["MonthBudget"].DataType = typeof(decimal);
            dt.Columns["ProcessPercent"].DataType = typeof(decimal);
            dt.Columns["LastMonthActual"].DataType = typeof(decimal);
            dt.Columns["QoQ"].DataType = typeof(decimal);
            dt.Columns["YearActual"].DataType = typeof(decimal);
            dt.Columns["YearBudgetActual"].DataType = typeof(decimal);
            dt.Columns["YearPercent"].DataType = typeof(decimal);
            dt.Columns["QoQYear"].DataType = typeof(decimal);
            dt.Columns["YoYYear"].DataType = typeof(decimal);
            dt.Columns["OnYear"].DataType = typeof(int);
            dt.Columns["OnMonth"].DataType = typeof(int);


            string SQL_STATEMENT = string.Format(@"
                    select R.ItemId,R.HotelId,I.AccountType AS ItemEName ,I.Department AS ItemCName ,R.KPI,R.MonthActual,R.MonthBudget,
                    R.ProcessPercent,R.LastMonthActual,
                    R.QoQ,R.YearActual,R.YearBudgetActual,R.YearPercent,R.QoQYear,
                    R.YoYYear,R.OnYear,R.OnMonth
                    from dbo.RoomMarket R left join ReportItem I on R.ItemId = i.ID
                  where R.HotelId={0} and  R.OnYear ={1} and R.OnMonth={2}
                    ", mHotelId, mYear, mMonth);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();

                var itemName = row["ItemEName"].ToString();
                var eIndex = itemName.LastIndexOf('/');

                dr1["HotelId"] = row["HotelId"];
                dr1["ItemId"] = row["ItemId"];
                dr1["ItemEName"] = itemName.Substring(0, eIndex);
                dr1["ItemCName"] = itemName.Substring(eIndex+1);
                dr1["KPI"] = row["KPI"].ToString();
                dr1["MonthActual"] = row["MonthActual"];
                dr1["MonthBudget"] = row["MonthBudget"];
                dr1["ProcessPercent"] = row["ProcessPercent"];
                dr1["LastMonthActual"] = row["LastMonthActual"];
                dr1["QoQ"] = row["QoQ"];
                dr1["YearActual"] = row["YearActual"];
                dr1["YearBudgetActual"] = row["YearBudgetActual"];
                dr1["YearPercent"] = row["YearPercent"];

                dr1["QoQYear"] = row["QoQYear"];
                dr1["YoYYear"] = row["YoYYear"];
                dr1["OnYear"] = row["OnYear"];
                dr1["OnMonth"] = row["OnMonth"];
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet getRoomMarketCompanyPriceDS(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "ItemId", "ItemEName", "ItemCName", "KPI", "MonthActual", "LastMonthActual", "QoQ", "YearActual", "QoQYear", "YoYYear", "OnYear", "OnMonth" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(int);
            dt.Columns["ItemId"].DataType = typeof(int);

            dt.Columns["MonthActual"].DataType = typeof(decimal);
            dt.Columns["LastMonthActual"].DataType = typeof(decimal);
            dt.Columns["QoQ"].DataType = typeof(decimal);
            dt.Columns["YearActual"].DataType = typeof(decimal);

            dt.Columns["QoQYear"].DataType = typeof(decimal);
            dt.Columns["YoYYear"].DataType = typeof(decimal);
            dt.Columns["OnYear"].DataType = typeof(int);
            dt.Columns["OnMonth"].DataType = typeof(int);


            string SQL_STATEMENT = string.Format(@"
                    select R.ItemId,R.HotelId,I.AccountType AS ItemEName ,I.Department AS ItemCName ,R.KPI,R.MonthActual,
                    R.LastMonthActual,
                    R.QoQ,R.YearActual,R.QoQYear,
                    R.YoYYear,R.OnYear,R.OnMonth
                    from dbo.RoomMarket_CompanyPrice R left join ReportItem I on R.ItemId = i.ID
                  where R.HotelId={0} and  R.OnYear ={1} and R.OnMonth={2}
                    ", mHotelId, mYear, mMonth);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();

                dr1["HotelId"] = row["HotelId"];
                dr1["ItemId"] = row["ItemId"];
                dr1["ItemEName"] = row["ItemEName"].ToString();
                dr1["ItemCName"] = row["ItemEName"].ToString();
                dr1["KPI"] = row["KPI"].ToString();
                dr1["MonthActual"] = row["MonthActual"];
                dr1["LastMonthActual"] = row["LastMonthActual"];
                dr1["QoQ"] = row["QoQ"];
                dr1["YearActual"] = row["YearActual"];
                dr1["QoQYear"] = row["QoQYear"];
                dr1["YoYYear"] = row["YoYYear"];
                dr1["OnYear"] = row["OnYear"];
                dr1["OnMonth"] = row["OnMonth"];
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// Get Team Group Detail for SZ JW
        /// </summary>
        /// <param name="mHotelId"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <returns></returns>
        public DataSet getRoomMarketGroupDetail(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "ItemId", "ItemEName", "ItemCName", "KPI", "MonthActual", "MonthBudget", "ProcessPercent", "LastMonthActual", "QoQ", "YearActual", "YearBudgetActual", "YearPercent", "QoQYear", "YoYYear", "OnYear", "OnMonth" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(int);
            dt.Columns["ItemId"].DataType = typeof(int);

            dt.Columns["MonthActual"].DataType = typeof(decimal);
            dt.Columns["MonthBudget"].DataType = typeof(decimal);
            dt.Columns["ProcessPercent"].DataType = typeof(decimal);
            dt.Columns["LastMonthActual"].DataType = typeof(decimal);
            dt.Columns["QoQ"].DataType = typeof(decimal);
            dt.Columns["YearActual"].DataType = typeof(decimal);
            dt.Columns["YearBudgetActual"].DataType = typeof(decimal);
            dt.Columns["YearPercent"].DataType = typeof(decimal);
            dt.Columns["QoQYear"].DataType = typeof(decimal);
            dt.Columns["YoYYear"].DataType = typeof(decimal);
            dt.Columns["OnYear"].DataType = typeof(int);
            dt.Columns["OnMonth"].DataType = typeof(int);


            string SQL_STATEMENT = string.Format(@"
                    select R.ItemId,R.HotelId,I.AccountType AS ItemEName ,I.Department AS ItemCName ,R.KPI,R.MonthActual,R.MonthBudget,
                    R.ProcessPercent,R.LastMonthActual,
                    R.QoQ,R.YearActual,R.YearBudgetActual,R.YearPercent,R.QoQYear,
                    R.YoYYear,R.OnYear,R.OnMonth
                    from dbo.RoomMarket_GroupDetail R left join ReportItem I on R.ItemId = i.ID
                  where R.HotelId={0} and  R.OnYear ={1} and R.OnMonth={2}
                    ", mHotelId, mYear, mMonth);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();



                dr1["HotelId"] = row["HotelId"];
                dr1["ItemId"] = row["ItemId"];
                dr1["ItemEName"] = (row["ItemEName"].ToString().Split('/'))[0];
                dr1["ItemCName"] = (row["ItemEName"].ToString().Split('/'))[1];
                dr1["KPI"] = row["KPI"].ToString();
                dr1["MonthActual"] = row["MonthActual"];
                dr1["MonthBudget"] = row["MonthBudget"];
                dr1["ProcessPercent"] = row["ProcessPercent"];
                dr1["LastMonthActual"] = row["LastMonthActual"];
                dr1["QoQ"] = row["QoQ"];
                dr1["YearActual"] = row["YearActual"];
                dr1["YearBudgetActual"] = row["YearBudgetActual"];
                dr1["YearPercent"] = row["YearPercent"];

                dr1["QoQYear"] = row["QoQYear"];
                dr1["YoYYear"] = row["YoYYear"];
                dr1["OnYear"] = row["OnYear"];
                dr1["OnMonth"] = row["OnMonth"];
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }
    }
}
