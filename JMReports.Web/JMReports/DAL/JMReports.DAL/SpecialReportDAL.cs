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
    public partial class SpecialReportDAL : DataAccessComponent
    {


        public DataSet getSpecialReportDS(string mHotelId,string mYear, string mMonth)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("{0}, Start", DateTime.Now.ToString()));
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "Item1", "Item2", "MonthActual", "MonthBudget", "ProcessPercent", "LastMonthActual", "QoQ", "LastYear", "YoY", "YearAct", "SameYearBudget", "YearPercent", "QoQYear", "YoYYear", "YearBudget", "YearBudgetPercent", "Orderid", "ItemId", "M1", "M2", "M3", "M4", "M5", "M6" };
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
            dt.Columns["M1"].DataType = typeof(decimal);
            dt.Columns["M2"].DataType = typeof(decimal);
            dt.Columns["M3"].DataType = typeof(decimal); 
            dt.Columns["M4"].DataType = typeof(decimal);
            dt.Columns["M5"].DataType = typeof(decimal);
            dt.Columns["M6"].DataType = typeof(decimal);

            System.Diagnostics.Trace.WriteLine(string.Format("{0}, Begin to construct SQL statement", DateTime.Now.ToString()));
            string SQL_STATEMENT = string.Empty;

            string baseDT = string.Format("{0}-{1}-{2}", mYear, mMonth, "01");
            //上海金茂
            if (mHotelId == "1")
            {
                SQL_STATEMENT = string.Format(@"
                declare @baseDT as datetime = '{2}'
                    SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[vwSpecialReport1].[ItemId]
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
                      ,[MockName1]
                      ,[MockName2]
                      ,SF.M1
                      ,SF.M2
                      ,SF.M3
                      ,SF.M4
                      ,SF.M5
                      ,SF.M6
                      
                  FROM [Repository].[dbo].[vwSpecialReport1] left join 
                  (
                  select ItemId, Sum([1]) M1, SUM([2]) M2, SUM([3]) M3, SUM([4]) M4, SUM([5]) M5, SUM([6]) M6
from
(
select HotelId, ItemId,[1], [2],[3],[4],[5],[6]
from (
SELECT    ROW_NUMBER() Over(Partition by ItemId order by MonthDate asc) RowNumber, * 
FROM         ActMonthly2_special_forcast where MonthDate > @baseDT and MonthDate <= DATEADD(m,6, @baseDT) and HotelId = 1


) as SourceTable
pivot
(
Sum(Act)
  for RowNumber in ([1],[2],[3],[4],[5],[6])
) u
) A group by ItemId
                  ) SF on SF.ItemId = [vwSpecialReport1].ItemId
                  where yearCode={0} and monthCode={1} or yearCode is null
                  --where yearCode=2014 and monthCode=1
                    ", mYear, mMonth, baseDT);
            }
            //崇明凯悦
            if (mHotelId == "2")
            {
                SQL_STATEMENT = string.Format(@"
                declare @baseDT as datetime = '{2}'

SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[vwSpecialReport2].[ItemId]
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
                      ,[MockName1]
                      ,[MockName2]
                      ,SF.M1
                      ,SF.M2
                      ,SF.M3
                      ,SF.M4
                      ,SF.M5
                      ,SF.M6
                      
                  FROM [Repository].[dbo].[vwSpecialReport2] left join 
                  (
                  select ItemId, Sum([1]) M1, SUM([2]) M2, SUM([3]) M3, SUM([4]) M4, SUM([5]) M5, SUM([6]) M6
from
(
select HotelId, ItemId,[1], [2],[3],[4],[5],[6]
from (
SELECT    ROW_NUMBER() Over(Partition by ItemId order by MonthDate asc) RowNumber, * 
FROM         ActMonthly2_special_forcast where MonthDate > @baseDT and MonthDate <= DATEADD(m,6, @baseDT) and HotelId = 2


) as SourceTable
pivot
(
Sum(Act)
  for RowNumber in ([1],[2],[3],[4],[5],[6])
) u
) A group by ItemId
                  ) SF on SF.ItemId = [vwSpecialReport2].ItemId
                  where yearCode={0} and monthCode={1} or yearCode is null
                  --where yearCode=2014 and monthCode=1
                    ", mYear, mMonth, baseDT);
            }


            //深圳万豪
            if (mHotelId == "3")
            {
                SQL_STATEMENT = string.Format(@"
                declare @baseDT as datetime = '{2}'

SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[vwSpecialReport3].[ItemId]
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
                      ,[MockName1]
                      ,[MockName2]
                      ,SF.M1
                      ,SF.M2
                      ,SF.M3
                      ,SF.M4
                      ,SF.M5
                      ,SF.M6
                      
                  FROM [Repository].[dbo].[vwSpecialReport3] left join 
                  (
                  select ItemId, Sum([1]) M1, SUM([2]) M2, SUM([3]) M3, SUM([4]) M4, SUM([5]) M5, SUM([6]) M6
from
(
select HotelId, ItemId,[1], [2],[3],[4],[5],[6]
from (
SELECT    ROW_NUMBER() Over(Partition by ItemId order by MonthDate asc) RowNumber, * 
FROM         ActMonthly2_special_forcast where MonthDate > @baseDT and MonthDate <= DATEADD(m,6, @baseDT) and HotelId = 3


) as SourceTable
pivot
(
Sum(Act)
  for RowNumber in ([1],[2],[3],[4],[5],[6])
) u
) A group by ItemId
                  ) SF on SF.ItemId = [vwSpecialReport3].ItemId
                  where yearCode={0} and monthCode={1} or yearCode is null
                  --where yearCode=2014 and monthCode=1
                    ", mYear, mMonth, baseDT);
            }

            System.Diagnostics.Trace.WriteLine(string.Format("{0}, Begin ExecuteDataSet", DateTime.Now.ToString()));

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            System.Diagnostics.Trace.WriteLine(string.Format("{0}, End of ExecuteDataSet", DateTime.Now.ToString()));
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();
                dr1["Item1"] = row["MockName1"].ToString();
                dr1["Item2"] = row["MockName2"].ToString();

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
                dr1["M1"] = row["M1"];
                dr1["M2"] = row["M2"];
                dr1["M3"] = row["M3"];
                dr1["M4"] = row["M4"];
                dr1["M5"] = row["M5"];
                dr1["M6"] = row["M6"];
                dt.Rows.Add(dr1);
            }
            System.Diagnostics.Trace.WriteLine(string.Format("{0}, End of Constructing DataTable", DateTime.Now.ToString()));
            ds.Tables.Add(dt);
            return ds;
        }

    }
}
