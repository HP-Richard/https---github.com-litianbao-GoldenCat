using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;
using JMReports.LinqToSql;

namespace JMReports.WebApp.ReportBussiness
{
  public class DataImportReport
  {
    public System.Data.DataSet getDataImportReport(string HotelId, string mYear, string mMonth, string urlHeaderPath)
    {
      using (var context = Helper.GetDataContext())
      {
        var r = context.ExecuteQuery<DataImportTracking>(@"
                select d.*
                from DataImportTracking d join (
                select max(OperatorTime) ot, inputtablename  from dbo.DataImportTracking 
                where issuccess = 'true' and YearCode={0} and MonthCode={1} and HotelId={2} 
                group by inputtablename 
                ) tb on d.OperatorTime=tb.ot
                ", mYear, mMonth, HotelId);

        var r1 = from m in r
                 join u in context.SysUser on m.OperatorUserId equals u.Id
                 select
                   new
                   {
                     m.Id,
                     m.InputTableName,
                     m.OperatorTime,
                     FileName = m.IsSuccess.Value ? urlHeaderPath + @"/ScedXLS/" + m.FileName :
                      urlHeaderPath + @"/FailXLS/" + m.FileName,
                     u.UserId
                   };

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(Int32));
        dt.Columns.Add("InputTableName", typeof(string));
        dt.Columns.Add("OperatorTime", typeof(string));
        dt.Columns.Add("MonthDate", typeof(string));
        dt.Columns.Add("FileName", typeof(string));
        var m1 = r1.ToList();

        foreach (var c in m1)
        {
          dt.Rows.Add(c.Id, c.InputTableName, c.OperatorTime.ToString("yyyy-MM-dd"), c.UserId,
           c.FileName);
        }
        var ds = new DataSet();
        ds.Tables.Add(dt);
        return ds;
      }
    }

    public System.Data.DataSet getDataNotImportReport(string HotelId, string mYear, string mMonth)
    {
      using (var context = Helper.GetDataContext())
      {
        var r = context.ExecuteQuery<ImportReport>(@"
                select r.* from ImportReport r left join(
                select * from dbo.DataImportTracking 
                where issuccess = 'true' and YearCode={0} and MonthCode={1} and HotelId={2}
                ) dt
                on r.ImportReportName=dt.InputTableName where dt.Id is null and r.Kind='实际类'
                ", mYear, mMonth, HotelId);

        var m1 = r.ToList();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(Int32));
        dt.Columns.Add("ImportReportName", typeof(string));
        dt.Columns.Add("Kind", typeof(string));

        foreach (var c in m1)
        {
          dt.Rows.Add(c.Id,c.ImportReportName,c.Kind);
        }
        var ds = new DataSet();
        ds.Tables.Add(dt);
        return ds;
      }
    }

    public System.Data.DataSet getDataImportBudgetReport(string HotelId, string mYear, string urlHeaderPath)
    {
      using (var context = Helper.GetDataContext())
      {
        var r = context.ExecuteQuery<DataImportBudgetTracking>(@"
                select d.*
                from DataImportTracking d join (
                select max(OperatorTime) ot, inputtablename  from dbo.DataImportTracking 
                where issuccess = 'true' and MonthCode is null and YearCode={0} and HotelId={1} 
                group by inputtablename 
                ) tb on d.OperatorTime=tb.ot
                ", mYear, HotelId);

        var r1 = from m in r
                 join u in context.SysUser on m.OperatorUserId equals u.Id
                 select
                   new
                   {
                     m.Id,
                     m.InputTableName,
                     m.OperatorTime,
                     FileName = m.IsSuccess.Value ? urlHeaderPath + @"/ScedXLS/" + m.FileName :
                      urlHeaderPath + @"/FailXLS/" + m.FileName,
                     u.UserId
                   };

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(Int32));
        dt.Columns.Add("InputTableName", typeof(string));
        dt.Columns.Add("OperatorTime", typeof(string));
        dt.Columns.Add("MonthDate", typeof(string));
        dt.Columns.Add("FileName", typeof(string));
        var m1 = r1.ToList();

        foreach (var c in m1)
        {
          dt.Rows.Add(c.Id, c.InputTableName, c.OperatorTime.ToString("yyyy-MM-dd"), c.UserId,
           c.FileName);
        }
        var ds = new DataSet();
        ds.Tables.Add(dt);
        return ds;
      }
    }

    public System.Data.DataSet getDataNotImportBudgetReport(string HotelId, string mYear)
    {
      using (var context = Helper.GetDataContext())
      {
        var r = context.ExecuteQuery<ImportReport>(@"
                select r.* from ImportReport r left join(
                select * from dbo.DataImportTracking 
                where issuccess = 'true' and YearCode={0} and MonthCode is null and HotelId={1}
                ) dt
                on r.ImportReportName=dt.InputTableName where dt.Id is null and r.Kind='预算类'
                ", mYear, HotelId);

        var m1 = r.ToList();

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(Int32));
        dt.Columns.Add("ImportReportName", typeof(string));
        dt.Columns.Add("Kind", typeof(string));

        foreach (var c in m1)
        {
          dt.Rows.Add(c.Id, c.ImportReportName, c.Kind);
        }
        var ds = new DataSet();
        ds.Tables.Add(dt);
        return ds;
      }
    }

  }
}