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
  public partial class RoomSalesReportDAL : DataAccessComponent
  {

    public DataSet getRoomSalesReportDS(string mHotelId, string mYear, string mMonth)
    {
      DataSet ds = new DataSet();
      DataTable dt = new DataTable();
      string[] fields = new string[] { "HotelId", "ItemId", "ItemEName", "ItemCName", "KPI", "MonthActual", "LastMonthActual", "QoQ", "YearActual", "QoQYear", "YoYYear","TopYoYYear", "OnYear", "OnMonth" };
      foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

      dt.Columns["HotelId"].DataType = typeof(int);
      dt.Columns["ItemId"].DataType = typeof(int);

      dt.Columns["MonthActual"].DataType = typeof(decimal);
      dt.Columns["LastMonthActual"].DataType = typeof(decimal);
      dt.Columns["QoQ"].DataType = typeof(decimal);
      dt.Columns["YearActual"].DataType = typeof(decimal);
      dt.Columns["QoQYear"].DataType = typeof(decimal);
      dt.Columns["YoYYear"].DataType = typeof(decimal);
      dt.Columns["TopYoYYear"].DataType = typeof(decimal);
      dt.Columns["OnYear"].DataType = typeof(int);
      dt.Columns["OnMonth"].DataType = typeof(int);


      string SQL_STATEMENT = string.Format(@"
                    select R.ItemId,R.HotelId,IsNull(I.AccountType,R.ItemEName) AS ItemEName ,Isnull(I.AccountType,R.ItemEName) AS ItemCName,R.KPI,R.MonthActual,R.LastMonthActual,
                    R.QoQ,R.YearActual,R.QoQYear,
                    R.YoYYear, R.TopYoYYear,R.OnYear,R.OnMonth
                    from dbo.RoomSales R left join ReportItem I on R.ItemId = i.ID
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
        dr1["ItemEName"] = row["ItemEName"];
        dr1["ItemCName"] = row["ItemEName"];
        dr1["KPI"] = row["KPI"].ToString();
        dr1["MonthActual"] = row["MonthActual"];
        dr1["LastMonthActual"] = row["LastMonthActual"];
        dr1["QoQ"] = row["QoQ"];
        dr1["YearActual"] = row["YearActual"];

        dr1["QoQYear"] = row["QoQYear"];
        dr1["YoYYear"] = row["YoYYear"];
        dr1["TopYoYYear"] = row["TopYoYYear"];
        dr1["OnYear"] = row["OnYear"];
        dr1["OnMonth"] = row["OnMonth"];
        dt.Rows.Add(dr1);
      }

      ds.Tables.Add(dt);
      return ds;
    }

  }
}
