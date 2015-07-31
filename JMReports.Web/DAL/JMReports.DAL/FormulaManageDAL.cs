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
    public class FormulaManageDAL : DataAccessComponent
    {

        public DataSet getForecastReportDS(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "ItemId", "OnTheBook", "RoomCount", "AvgRoomPrice", "Revenue", "HotelId", "OnYear", "OnMonth" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["RoomCount"].DataType = typeof(int);
            dt.Columns["AvgRoomPrice"].DataType = typeof(decimal);
            dt.Columns["Revenue"].DataType = typeof(decimal);
            dt.Columns["HotelId"].DataType = typeof(int);
            dt.Columns["OnYear"].DataType = typeof(int);
            dt.Columns["OnMonth"].DataType = typeof(int);

            string SQL_STATEMENT = string.Empty;

            SQL_STATEMENT = string.Format(@"
            SELECT ItemId
                    ,OnTheBook
                    ,RoomCount
                    ,AvgRoomPrice
                    ,Revenue
                    ,HotelId
                    ,OnYear
                    ,OnMonth
                FROM Forecast
                where OnYear={0} and OnMonth={1} and HotelId={2}
                ", mYear, mMonth, mHotelId);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();

                dr1["ItemId"] = row["ItemId"];
                dr1["OnTheBook"] = row["OnTheBook"];
                dr1["RoomCount"] = row["RoomCount"];
                dr1["AvgRoomPrice"] = row["AvgRoomPrice"];
                dr1["Revenue"] = row["Revenue"];
                dr1["HotelId"] = row["HotelId"];
                dr1["OnYear"] = row["OnYear"];
                dr1["OnMonth"] = row["OnMonth"];

                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }


        public DataSet getFormula(string mYearCode, string mHotelId, string mAccountItemId)
        {


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string SQL_STATEMENT = string.Empty;

            //金茂君悦，崇明凯悦
            if (mHotelId == "1" || mHotelId == "2")
            {
                SQL_STATEMENT = string.Format(@"
                select r.ID,a.ItemId,r.AccountType,r.Department,a.Account,a.CostCenter,a.YearCode,a.HotelId,h.ChineseName as HotelName
                from AccountItem a 
	                left join ReportItem r on a.ItemId= r.ID
                    left join Hotel h on a.HotelId= h.HotelId
                where a.yearCode={0} and a.HotelId={1} and a.ItemId={2} ", mYearCode, mHotelId, mAccountItemId);


                //string[] fields = new string[] { "ItemId", "AccountType", "Department", "Account", "CostCenter", "yearCode", "HotelId" };
                //foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));


                Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

                ds = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT); 
            }


            //深圳JW万豪
            if (mHotelId == "3")
            {
                SQL_STATEMENT = string.Format(@"
                select r.ID,a.ItemId,r.AccountType,r.Department,a.Account_From,a.Account_To,a.CostCenter_From,a.CostCenter_To,a.YearCode,a.HotelId,h.ChineseName as HotelName 
                from AccountItem_SUN a 
                    left join ReportItem r on a.ItemId= r.ID
                    left join Hotel h on a.HotelId= h.HotelId
                where a.yearCode={0} and a.HotelId={1} and a.ItemId={2} ", mYearCode, mHotelId, mAccountItemId);

                Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

                ds = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT); 

            }

            return ds;
        }


    }
}
