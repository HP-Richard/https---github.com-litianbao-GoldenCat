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
    public partial class RoomCompeteDAL : DataAccessComponent
    {

        public DataSet getCombinationDS(string mHotelId, string mYear, string mMonth)
        {
            string SQL_STATEMENT = string.Format(@"
                      select Combination, OnYear, OnMonth, HotelId
                        from RoomCompete 
                        where HotelId={0} and OnYear ={1} and OnMonth={2} group by Combination, OnYear, OnMonth, HotelId
                    ", mHotelId, mYear, mMonth);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            return ds1;
        }

        public DataSet getRoomCompeteCurrentMonthDS(string mHotelId, string mYear, string mMonth, string combination)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "ItemId", "OnYear", "OnMonth", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "Combination", "SubGroup" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(int);
            dt.Columns["Itemid"].DataType = typeof(int);

            dt.Columns["D"].DataType = typeof(decimal);
            dt.Columns["E"].DataType = typeof(decimal);
            dt.Columns["F"].DataType = typeof(decimal);
            dt.Columns["G"].DataType = typeof(decimal);

            dt.Columns["H"].DataType = typeof(decimal);
            dt.Columns["I"].DataType = typeof(decimal);
            dt.Columns["J"].DataType = typeof(decimal);
            dt.Columns["K"].DataType = typeof(decimal);

            dt.Columns["L"].DataType = typeof(decimal);
            dt.Columns["M"].DataType = typeof(decimal);
            //dt.Columns["N"].DataType = typeof(decimal);
   

            dt.Columns["OnYear"].DataType = typeof(int);
            dt.Columns["OnMonth"].DataType = typeof(int);


            string SQL_STATEMENT = string.Format(@"
                      select [HotelId]
                              ,[ItemId]
                              ,[OnYear]
                              ,[OnMonth]
                              ,[B]
                              ,[C]
                              ,[D]
                              ,[E]
                              ,[F]
                              ,[G]
                              ,[H]
                              ,[I]
                              ,[J]
                              ,[K]
                              ,[L]
                              ,[M]
                              ,[N]
                              ,[combination]
                              ,[SubGroup]
                        from RoomCompete2
                        where HotelId={0} and OnYear ={1} and OnMonth={2} and combination = N'{3}'
                    ", mHotelId, mYear, mMonth, combination);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();

                dr1["HotelId"] = row["HotelId"];
                dr1["Itemid"] = row["Itemid"];
                dr1["Combination"] = row["Combination"].ToString();
                dr1["SubGroup"] = row["SubGroup"].ToString();

                dr1["B"] = row["B"].ToString();
                dr1["C"] = row["C"];
                dr1["D"] = row["D"];
                dr1["E"] = row["E"];

                dr1["F"] = row["F"];
                dr1["G"] = row["G"];
                dr1["H"] = row["H"];
                dr1["I"] = row["I"];

                dr1["J"] = row["J"];
                dr1["K"] = row["K"];
                dr1["L"] = row["L"];
                dr1["M"] = row["M"];
                dr1["N"] = row["N"];

                dr1["OnYear"] = row["OnYear"];
                dr1["OnMonth"] = row["OnMonth"];

                dt.Rows.Add(dr1);

            }

            ds.Tables.Add(dt);
            return ds;
        }
        public DataSet getRoomCompeteDS(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "Itemid", "Combination", "STR1", "Occupancy_MyProp", "Occupancy_CompSet", 
                "Occupancy_Index", "Occupancy_MPI", "ADR_MyProp", "ADR_CompSet", "ADR_Index", "ADR_MPI", "RevPAR_MyProp", 
                "RevPAR_CompSet", "RevPAR_Index", "RevPAR_MPI", "OnYear", "OnMonth" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(int);
            dt.Columns["Itemid"].DataType = typeof(int);

            dt.Columns["Occupancy_MyProp"].DataType = typeof(decimal);
            dt.Columns["Occupancy_CompSet"].DataType = typeof(decimal);
            dt.Columns["Occupancy_Index"].DataType = typeof(decimal);
            dt.Columns["Occupancy_MPI"].DataType = typeof(decimal);

            dt.Columns["ADR_MyProp"].DataType = typeof(decimal);
            dt.Columns["ADR_CompSet"].DataType = typeof(decimal);
            dt.Columns["ADR_Index"].DataType = typeof(decimal);
            dt.Columns["ADR_MPI"].DataType = typeof(decimal);

            dt.Columns["RevPAR_MyProp"].DataType = typeof(decimal);
            dt.Columns["RevPAR_CompSet"].DataType = typeof(decimal);
            dt.Columns["RevPAR_Index"].DataType = typeof(decimal);
            dt.Columns["RevPAR_MPI"].DataType = typeof(decimal);

            dt.Columns["OnYear"].DataType = typeof(int);
            dt.Columns["OnMonth"].DataType = typeof(int);


            string SQL_STATEMENT = string.Format(@"
                      select Id,HotelId,Itemid,Combination,STR1,Occupancy_MyProp,Occupancy_CompSet,Occupancy_Index,Occupancy_MPI,ADR_MyProp,ADR_CompSet,ADR_Index,ADR_MPI,RevPAR_MyProp,RevPAR_CompSet,RevPAR_Index,RevPAR_MPI,OnYear,OnMonth,Decorate
                        from RoomCompete
                        where HotelId={0} and OnYear ={1} and OnMonth={2} and Decorate={3}
                    ", mHotelId, mYear, mMonth,"1");

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();

                dr1["HotelId"] = row["HotelId"];
                dr1["Itemid"] = row["Itemid"];
                dr1["Combination"] = row["Combination"].ToString();
                dr1["STR1"] = row["STR1"].ToString();

                dr1["Occupancy_MyProp"] = row["Occupancy_MyProp"].ToString();
                dr1["Occupancy_CompSet"] = row["Occupancy_CompSet"];
                dr1["Occupancy_Index"] = row["Occupancy_Index"];
                dr1["Occupancy_MPI"] = row["Occupancy_MPI"];

                dr1["ADR_MyProp"] = row["ADR_MyProp"];
                dr1["ADR_CompSet"] = row["ADR_CompSet"];
                dr1["ADR_Index"] = row["ADR_Index"];
                dr1["ADR_MPI"] = row["ADR_MPI"];

                dr1["RevPAR_MyProp"] = row["RevPAR_MyProp"];
                dr1["RevPAR_CompSet"] = row["RevPAR_CompSet"];
                dr1["RevPAR_Index"] = row["RevPAR_Index"];
                dr1["RevPAR_MPI"] = row["RevPAR_MPI"];

                dr1["OnYear"] = row["OnYear"];
                dr1["OnMonth"] = row["OnMonth"];

                dt.Rows.Add(dr1);

            }

            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet getRoomCompeteDS2(string mHotelId, string mYear, string mMonth, string combination)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "HotelId", "Itemid", "Combination", "STR1", "Occupancy_MyProp", "Occupancy_CompSet", 
                "Occupancy_Index", "Occupancy_MPI", "ADR_MyProp", "ADR_CompSet", "ADR_Index", "ADR_MPI", "RevPAR_MyProp", 
                "RevPAR_CompSet", "RevPAR_Index", "RevPAR_MPI", "OnYear", "OnMonth" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));

            dt.Columns["HotelId"].DataType = typeof(int);
            dt.Columns["Itemid"].DataType = typeof(int);

            dt.Columns["Occupancy_MyProp"].DataType = typeof(decimal);
            dt.Columns["Occupancy_CompSet"].DataType = typeof(decimal);
            dt.Columns["Occupancy_Index"].DataType = typeof(decimal);
            dt.Columns["Occupancy_MPI"].DataType = typeof(decimal);

            dt.Columns["ADR_MyProp"].DataType = typeof(decimal);
            dt.Columns["ADR_CompSet"].DataType = typeof(decimal);
            dt.Columns["ADR_Index"].DataType = typeof(decimal);
            dt.Columns["ADR_MPI"].DataType = typeof(decimal);

            dt.Columns["RevPAR_MyProp"].DataType = typeof(decimal);
            dt.Columns["RevPAR_CompSet"].DataType = typeof(decimal);
            dt.Columns["RevPAR_Index"].DataType = typeof(decimal);
            dt.Columns["RevPAR_MPI"].DataType = typeof(decimal);

            dt.Columns["OnYear"].DataType = typeof(int);
            dt.Columns["OnMonth"].DataType = typeof(int);


            string SQL_STATEMENT = string.Format(@"
                      select Id,HotelId,Itemid,Combination,STR1,Occupancy_MyProp,Occupancy_CompSet,Occupancy_Index,Occupancy_MPI,ADR_MyProp,ADR_CompSet,ADR_Index,ADR_MPI,RevPAR_MyProp,RevPAR_CompSet,RevPAR_Index,RevPAR_MPI,OnYear,OnMonth,Decorate
                        from RoomCompete
                        where HotelId={0} and OnYear ={1} and OnMonth={2} and Combination=N'{3}'
                    ", mHotelId, mYear, mMonth, combination);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();

                dr1["HotelId"] = row["HotelId"];
                dr1["Itemid"] = row["Itemid"];
                dr1["Combination"] = row["Combination"].ToString();
                dr1["STR1"] = row["STR1"].ToString();

                dr1["Occupancy_MyProp"] = row["Occupancy_MyProp"];
                dr1["Occupancy_CompSet"] = row["Occupancy_CompSet"];
                dr1["Occupancy_Index"] = row["Occupancy_Index"];
                dr1["Occupancy_MPI"] = row["Occupancy_MPI"];

                dr1["ADR_MyProp"] = row["ADR_MyProp"];
                dr1["ADR_CompSet"] = row["ADR_CompSet"];
                dr1["ADR_Index"] = row["ADR_Index"];
                dr1["ADR_MPI"] = row["ADR_MPI"];

                dr1["RevPAR_MyProp"] = row["RevPAR_MyProp"];
                dr1["RevPAR_CompSet"] = row["RevPAR_CompSet"];
                dr1["RevPAR_Index"] = row["RevPAR_Index"];
                dr1["RevPAR_MPI"] = row["RevPAR_MPI"];

                dr1["OnYear"] = row["OnYear"];
                dr1["OnMonth"] = row["OnMonth"];

                dt.Rows.Add(dr1);

            }

            ds.Tables.Add(dt);
            return ds;
        }
    }
}
