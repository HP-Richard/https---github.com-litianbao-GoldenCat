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
    public partial class RestaurantReportDAL : DataAccessComponent
    {


        public DataSet getRestaurantVarDS(DateTime dt1, string HotelId , string ResturantId,string mYear,string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "RestaurantName", "RestaurantChineseName","Item1", "MonthActual", "MonthBudget", "ProcessPercent", "LastYear", "YoY", "YearAct", "AccuBudget", "FinishPercent", "LastYear_Bud", "YoY_Bud", "Orderid"};
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));
            dt.Columns["MonthActual"].DataType = typeof(decimal);
            dt.Columns["MonthBudget"].DataType = typeof(decimal);
            dt.Columns["ProcessPercent"].DataType = typeof(decimal);
            dt.Columns["LastYear"].DataType = typeof(decimal);
            dt.Columns["YoY"].DataType = typeof(decimal);
            dt.Columns["YearAct"].DataType = typeof(decimal);
            dt.Columns["AccuBudget"].DataType = typeof(decimal);
            dt.Columns["FinishPercent"].DataType = typeof(decimal);
            dt.Columns["LastYear_Bud"].DataType = typeof(decimal);
            dt.Columns["YoY_Bud"].DataType = typeof(decimal);
            dt.Columns["Orderid"].DataType = typeof(int);

            string strSql = string.Empty;
            if (ResturantId == "0")
            {
                strSql = string.Format(@"
                    select H.ChineseName hotelName,B.Name RestaurantName,B.ChineseName RestaurantChineseName,A.Id,A.HotelId,A.RestaurantId,
                    A.Revenue*-1 Revenue,
                    A.Food_Revenue * -1 Food_Revenue,
                    A.BEV_Revenue *-1 BEV_Revenue,
                    A.Food_Cost,A.BEV_Cost,A.Other_Cost,A.Benefits,A.Expense,A.Profit,
                    A.ProfitRate,A.Food_CostRate,A.BEV_CostRate,A.Covers,A.OnYear,A.OnMonth,A.CreateDate  
                    from RestaurantSummary A left join Restaurant B ON A.RestaurantId = B.id  
                    Left join Hotel H ON A.HotelId = H.HotelId 
                    where H.HotelId={0} and A.OnYear={1} and A.OnMonth={2}
                    ",HotelId, mYear,mMonth);
            }
            else
            {
                strSql = string.Format(@"
                    select H.ChineseName hotelName,B.Name RestaurantName,B.ChineseName RestaurantChineseName,A.Id,A.HotelId,A.RestaurantId,
                    A.Revenue*-1 Revenue,
                    A.Food_Revenue * -1 Food_Revenue,
                    A.BEV_Revenue *-1 BEV_Revenue,
                    A.Food_Cost,A.BEV_Cost,A.Other_Cost,A.Benefits,A.Expense,A.Profit,
                    A.ProfitRate,A.Food_CostRate,A.BEV_CostRate,A.Covers,A.OnYear,A.OnMonth,A.CreateDate  
                    from RestaurantSummary A left join Restaurant B ON A.RestaurantId = B.id  
                    Left join Hotel H ON A.HotelId = H.HotelId 
                    where H.HotelId={0} and B.id={1} and A.OnYear={2} and A.OnMonth={3}
                    ", HotelId,ResturantId,mYear,mMonth);
            }

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, strSql);
            DataRow dr1 = null;

            //计算预算数据
            strSql = string.Format(@"
                    select H.ChineseName hotelName,B.ChineseName RestaurantName,A.Id,A.HotelId,A.RestaurantId,
                    A.Revenue*-1 Revenue,
                    A.Food_Revenue * -1 Food_Revenue,
                    A.BEV_Revenue *-1 BEV_Revenue,
                    A.Food_Cost,A.BEV_Cost,A.Other_Cost,A.Benefits,A.Expense,A.Profit,
                    A.ProfitRate,A.Food_CostRate,A.BEV_CostRate,A.Covers,A.OnYear,A.OnMonth,A.CreateDate  
                    from RestaurantBudgetSummary A left join Restaurant B ON A.RestaurantId = B.id  
                    Left join Hotel H ON A.HotelId = H.HotelId 
                    where H.HotelId={0} and A.OnYear={1} and A.OnMonth={2}
                    ", HotelId, mYear, mMonth);

            DataSet ds_budget = db.ExecuteDataSet(CommandType.Text, strSql);
            DataTable budgetTable = ds_budget.Tables[0];


            //计算上年数据
            strSql = string.Format(@"
                   select H.ChineseName hotelName,B.ChineseName RestaurantName,A.Id,A.HotelId,A.RestaurantId,
                    A.Revenue*-1 Revenue,
                    A.Food_Revenue * -1 Food_Revenue,
                    A.BEV_Revenue *-1 BEV_Revenue,
                    A.Food_Cost,A.BEV_Cost,A.Other_Cost,A.Benefits,A.Expense,A.Profit,
                    A.ProfitRate,A.Food_CostRate,A.BEV_CostRate,A.Covers,A.OnYear,A.OnMonth,A.CreateDate  
                    from RestaurantBudgetSummary A left join Restaurant B ON A.RestaurantId = B.id  
                    Left join Hotel H ON A.HotelId = H.HotelId 
                    where H.HotelId={0} and A.OnYear={1} and A.OnMonth={2}
                    ", HotelId, (int.Parse(mYear)-1).ToString(), mMonth);

            DataSet ds_LastYear = db.ExecuteDataSet(CommandType.Text, strSql);
            DataTable dt_LastYear = ds_LastYear.Tables[0];


            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "收入(万元)";
                dr1["MonthActual"] = row["Revenue"];
                dr1["MonthBudget"] = getMonthValue(budgetTable, "Revenue", "1", row["RestaurantId"].ToString(), mYear, mMonth);

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = getMonthValue(dt_LastYear,"Revenue", "1", row["RestaurantId"].ToString(), (int.Parse(mYear)-1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = getYearAct("Revenue", "1", row["RestaurantId"].ToString(), "2014");
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 1;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "食品成本(万元)";
                dr1["MonthActual"] = row["Food_Cost"];
                dr1["MonthBudget"] = getMonthValue(budgetTable, "Food_Cost", "1", row["RestaurantId"].ToString(), mYear, mMonth);
                //dr1["ProcessPercent"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = getMonthValue(dt_LastYear, "Food_Cost", "1", row["RestaurantId"].ToString(), (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = getYearAct("Food_Cost", "1", row["RestaurantId"].ToString(), "2014");
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 2;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "酒水成本(万元)";
                dr1["MonthActual"] = row["BEV_Cost"];
                dr1["MonthBudget"] = getMonthValue(budgetTable, "BEV_Cost", "1", row["RestaurantId"].ToString(), mYear, mMonth);
                //dr1["ProcessPercent"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }

                dr1["LastYear"] = getMonthValue(dt_LastYear, "BEV_Cost", "1", row["RestaurantId"].ToString(), (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = getYearAct("BEV_Cost", "1", row["RestaurantId"].ToString(), "2014");
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 3;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "其他成本(万元)";
                dr1["MonthActual"] = row["Other_Cost"];
                dr1["MonthBudget"] = getMonthValue(budgetTable, "Other_Cost", "1", row["RestaurantId"].ToString(), mYear, mMonth);
                //dr1["ProcessPercent"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = getMonthValue(dt_LastYear, "Other_Cost", "1", row["RestaurantId"].ToString(), (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = getYearAct("Other_Cost", "1", row["RestaurantId"].ToString(), "2014");
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 4;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "工资及福利(万元)";
                dr1["MonthActual"] = row["Benefits"];
                dr1["MonthBudget"] = getMonthValue(budgetTable, "Benefits", "1", row["RestaurantId"].ToString(), mYear, mMonth);
                //dr1["ProcessPercent"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = getMonthValue(dt_LastYear, "Benefits", "1", row["RestaurantId"].ToString(), (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = getYearAct("Benefits", "1", row["RestaurantId"].ToString(), "2014");
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 5;
                dt.Rows.Add(dr1);



                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "其他费用(万元)";
                dr1["MonthActual"] = row["Expense"];
                dr1["MonthBudget"] = getMonthValue(budgetTable, "Expense", "1", row["RestaurantId"].ToString(), mYear, mMonth);

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }

                dr1["LastYear"] = getMonthValue(dt_LastYear, "Expense", "1", row["RestaurantId"].ToString(), (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = getYearAct("Expense", "1", row["RestaurantId"].ToString(), "2014");
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 6;
                dt.Rows.Add(dr1);



                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "利润(万元)";
                dr1["MonthActual"] = row["Profit"];
                dr1["MonthBudget"] = getMonthValue(budgetTable, "Profit", "1", row["RestaurantId"].ToString(), mYear, mMonth);
                //dr1["ProcessPercent"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = getMonthValue(dt_LastYear, "Profit", "1", row["RestaurantId"].ToString(), (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = getYearAct("Profit", "1", row["RestaurantId"].ToString(), "2014");
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 7;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "餐饮利润率";
                dr1["MonthActual"] = row["ProfitRate"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 8;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "食品成本率";
                dr1["MonthActual"] = row["Food_CostRate"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 9;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "酒水成本率";
                dr1["MonthActual"] = row["BEV_CostRate"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 10;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "用餐人数(人次)";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 11;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "每单消费(元)";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 12;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "面积(平方米)";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 13;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "每平米收入(万元/平方米)";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 14;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "座位数(座次)";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 15;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["RestaurantName"] = row["RestaurantName"];
                dr1["RestaurantChineseName"] = row["RestaurantChineseName"];
                dr1["Item1"] = "每座收入(万元/座次)";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 16;
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            
            return ds;
        }


        public decimal getYearAct(string mAccount, string mHotelId, string mRestaurantId, string mYear)
        {
            string mStrSql = string.Format(@"select SUM({3}) * -1 as Revenue
                from dbo.RestaurantSummary
                where HotelId={0} and RestaurantId={1} and OnYear={2}",mHotelId,mRestaurantId,mYear,mAccount);

            decimal mReturnValue= 0.00m;
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            var dbValue = db.ExecuteScalar(CommandType.Text, mStrSql);

            if (dbValue is DBNull)
            {
                mReturnValue = 0.00m;
            }
            else
            {
                //mReturnValue = decimal.Parse(db.ExecuteScalar(CommandType.Text, mStrSql).ToString());

                mReturnValue = decimal.Parse(dbValue.ToString());
            }



            return mReturnValue;

        }

        public decimal getMonthValue(DataTable mTable, string mAccount, string mHotelId, string mRestaurantId, string mYear, string mMonth)
        {

            decimal mReturnValue = 0.00m;

            int v_resturantId = int.Parse(mRestaurantId);
            string v_account = mAccount;

            var query =
               from T in mTable.AsEnumerable()
               where T.Field<int>("RestaurantId") == v_resturantId && T.IsNull(v_account) == false
               select new
               {
                   Value_Account = T.Field<decimal>(v_account)
               };

            foreach (var b in query)
            {
                mReturnValue = b.Value_Account;
            }

            return mReturnValue;
        }

        public DataSet getRestaurantEfficiency(string HotelId, string mYear, string mMonth)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] {"Item1", "MonthActual", "MonthBudget", "ProcessPercent", "LastYear", "YoY", "YearAct", "AccuBudget", "FinishPercent", "LastYear_Bud", "YoY_Bud", "Orderid" };
            foreach (string f1 in fields) dt.Columns.Add(new DataColumn(f1));
            dt.Columns["MonthActual"].DataType = typeof(decimal);
            dt.Columns["MonthBudget"].DataType = typeof(decimal);
            dt.Columns["ProcessPercent"].DataType = typeof(decimal);
            dt.Columns["LastYear"].DataType = typeof(decimal);
            dt.Columns["YoY"].DataType = typeof(decimal);
            dt.Columns["YearAct"].DataType = typeof(decimal);
            dt.Columns["AccuBudget"].DataType = typeof(decimal);
            dt.Columns["FinishPercent"].DataType = typeof(decimal);
            dt.Columns["LastYear_Bud"].DataType = typeof(decimal);
            dt.Columns["YoY_Bud"].DataType = typeof(decimal);
            dt.Columns["Orderid"].DataType = typeof(int);

            
            string strSql = string.Empty;
                strSql = string.Format(@"
                    select A.Id,A.HotelId,H.ChineseName hotelName
					  ,A.Revenue
					  ,A.Restaurant_Revenue
					  ,A.Food_Revenue
					  ,A.BEV_Revenue
					  ,A.Other_Revenue
					  ,A.Restaurant_Percent
					  ,A.Restaurant_Cost
					  ,A.Food_Cost
					  ,A.Food_Cost_Percent
					  ,A.BEV_Cost
					  ,A.BEV_Cost_Percent
					  ,A.Other_Cost
					  ,A.Other_Cost_Percent
					  ,A.Labor_Cost
					  ,A.Labor_Cost_Percent
					  ,A.Restaurant_Dept_Expense
					  ,A.Restaurant_Dept_Expense_Percent
					  ,A.Restaurant_People
					  ,A.Consumption_People
					  ,A.OnYear
					  ,A.OnMonth
                    from RestaurantEfficiency A Left join Hotel H ON A.HotelId = H.HotelId
                    where A.HotelId = {0} and A.OnYear={1} and A.OnMonth={2}
                    ", HotelId, mYear,mMonth);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, strSql);
            DataRow dr1 = null;


            //计算预算数据
            strSql = string.Format(@"
                    select A.Id,A.HotelId,H.ChineseName hotelName
					  ,A.Revenue
					  ,A.Restaurant_Revenue
					  ,A.Food_Revenue
					  ,A.BEV_Revenue
					  ,A.Other_Revenue
					  ,A.Restaurant_Percent
					  ,A.Restaurant_Cost
					  ,A.Food_Cost
					  ,A.Food_Cost_Percent
					  ,A.BEV_Cost
					  ,A.BEV_Cost_Percent
					  ,A.Other_Cost
					  ,A.Other_Cost_Percent
					  ,A.Labor_Cost
					  ,A.Labor_Cost_Percent
					  ,A.Restaurant_Dept_Expense
					  ,A.Restaurant_Dept_Expense_Percent
					  ,A.Restaurant_People
					  ,A.Consumption_People
					  ,A.OnYear
					  ,A.OnMonth
                    from RestaurantEfficiency A Left join Hotel H ON A.HotelId = H.HotelId
                    where A.HotelId = {0} and A.OnYear={1} and A.OnMonth={2}
                    ", HotelId, mYear, mMonth);

            DataSet ds_budget = db.ExecuteDataSet(CommandType.Text, strSql);
            DataTable budgetTable = ds_budget.Tables[0];


            //计算上年数据
            strSql = string.Format(@"
                    select A.Id,A.HotelId,H.ChineseName hotelName
					  ,A.Revenue
					  ,A.Restaurant_Revenue
					  ,A.Food_Revenue
					  ,A.BEV_Revenue
					  ,A.Other_Revenue
					  ,A.Restaurant_Percent
					  ,A.Restaurant_Cost
					  ,A.Food_Cost
					  ,A.Food_Cost_Percent
					  ,A.BEV_Cost
					  ,A.BEV_Cost_Percent
					  ,A.Other_Cost
					  ,A.Other_Cost_Percent
					  ,A.Labor_Cost
					  ,A.Labor_Cost_Percent
					  ,A.Restaurant_Dept_Expense
					  ,A.Restaurant_Dept_Expense_Percent
					  ,A.Restaurant_People
					  ,A.Consumption_People
					  ,A.OnYear
					  ,A.OnMonth
                    from RestaurantEfficiency A Left join Hotel H ON A.HotelId = H.HotelId
                    where A.HotelId = {0} and A.OnYear={1} and A.OnMonth={2}
                    ", HotelId, (int.Parse(mYear)-1).ToString(), mMonth);

            DataSet ds_LastYear = db.ExecuteDataSet(CommandType.Text, strSql);
            DataTable dt_LastYear = ds_LastYear.Tables[0];


            //OE

            strSql = string.Format(@"
                    SELECT id
                          ,HotelID
                          ,OEBreakAmount
                          ,OEBreak_Chinaware
                          ,OEBreak_Silverware
                          ,OEBreak_Glassware
                          ,OEBreak_Linen
                          ,OnMonth
                          ,OnYear
                          ,CreateTime
                      FROM OEBreak
                      where hotelid={0} and onMonth={1} and onYear={2}", HotelId, mMonth,mYear);

            DataSet ds_OE = db.ExecuteDataSet(CommandType.Text, strSql);
            DataTable dt_OE = ds_OE.Tables[0];


            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();
                dr1["Item1"] = "酒店总收入";
                dr1["MonthActual"] = row["Revenue"];
                dr1["MonthBudget"] = getEfficiencyBudget(budgetTable, "Revenue",HotelId, mYear, mMonth);

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = getEfficiencyMonthValue(dt_LastYear, "Revenue", HotelId,  (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 1;
                dt.Rows.Add(dr1);



                dr1 = dt.NewRow();
                dr1["Item1"] = "餐饮收入";
                dr1["MonthActual"] = row["Restaurant_Revenue"];
                dr1["MonthBudget"] = getEfficiencyBudget(dt_LastYear, "Restaurant_Revenue", HotelId, (int.Parse(mYear) - 1).ToString(), mMonth);

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }

                dr1["LastYear"] = getEfficiencyMonthValue(dt_LastYear, "Restaurant_Revenue", HotelId, (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 2;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "其中：食品收入";
                dr1["MonthActual"] = row["Food_Revenue"];
                dr1["MonthBudget"] = getEfficiencyBudget(budgetTable, "BEV_Cost",HotelId, mYear, mMonth);

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }

                dr1["LastYear"] = getEfficiencyMonthValue(dt_LastYear, "Food_Revenue",HotelId, (int.Parse(mYear) - 1).ToString(), mMonth);
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 3;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "------酒水收入";
                dr1["MonthActual"] = row["BEV_Revenue"];
                dr1["MonthBudget"] = "0.00";
                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0.00") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = 0.00;

                dr1["YoY"] = 0.00;
                dr1["YearAct"] =0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 4;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "------其他收入";
                dr1["MonthActual"] = row["Other_Revenue"];
                dr1["MonthBudget"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 5;
                dt.Rows.Add(dr1);



                dr1 = dt.NewRow();
                dr1["Item1"] = "餐饮收入占总收入%";
                dr1["MonthActual"] = row["Restaurant_Percent"];
                dr1["MonthBudget"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }

                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 6;
                dt.Rows.Add(dr1);



                dr1 = dt.NewRow();
                dr1["Item1"] = "餐饮成本";
                dr1["MonthActual"] = row["Restaurant_Cost"];
                dr1["MonthBudget"] = 0.00;
                //dr1["ProcessPercent"] = 0.00;

                if ((dr1["MonthBudget"] is DBNull) || (dr1["MonthBudget"].ToString() == "0") || (dr1["MonthBudget"].ToString() == ""))
                {
                    dr1["ProcessPercent"] = 0.00;
                }
                else
                {
                    dr1["ProcessPercent"] = (decimal.Parse(dr1["MonthActual"].ToString()) / decimal.Parse(dr1["MonthBudget"].ToString())) * 100;
                }
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 7;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "其中：食品成本";
                dr1["MonthActual"] = row["Food_Cost"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 8;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "             食品成本%";
                dr1["MonthActual"] = row["Food_Cost_Percent"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 9;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "          酒水成本";
                dr1["MonthActual"] = row["BEV_Cost"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 10;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "             酒水成本%";
                dr1["MonthActual"] = row["BEV_Cost_Percent"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 11;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "          其他成本";
                dr1["MonthActual"] = row["Other_Cost"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 12;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "             其他成本%";
                dr1["MonthActual"] = row["Other_Cost_Percent"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 13;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "人工成本";
                dr1["MonthActual"] = row["Labor_Cost"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 14;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "人工成本%";
                dr1["MonthActual"] = row["Labor_Cost_Percent"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 15;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "餐饮费用";
                dr1["MonthActual"] = row["Restaurant_Dept_Expense"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 16;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "餐饮费用%";
                dr1["MonthActual"] = row["Restaurant_Dept_Expense_Percent"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 17;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "餐饮利润";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 18;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "OE破损总数";
                dr1["MonthActual"] = getOEValue(dt_OE, "OEBreakAmount");
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 19;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "OE破损%";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 20;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "其中： 瓷器破损";
                dr1["MonthActual"] = getOEValue(dt_OE, "OEBreak_Chinaware");
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 21;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "               瓷器破损%";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 22;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "       银器";
                dr1["MonthActual"] = getOEValue(dt_OE, "OEBreak_Silverware");
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 23;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "                银器破损%";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 24;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "       玻璃器皿";
                dr1["MonthActual"] = getOEValue(dt_OE, "OEBreak_Glassware");
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 25;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "                玻璃器皿破损%";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 26;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "       布草破损";
                dr1["MonthActual"] = getOEValue(dt_OE, "OEBreak_Linen"); ;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 27;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "                布草破损%";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 28;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "餐厅消费人数";
                dr1["MonthActual"] = row["Restaurant_People"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 29;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "人均消费";
                dr1["MonthActual"] = row["Consumption_People"];
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 30;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "OE破损/用餐人数";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 31;
                dt.Rows.Add(dr1);

                dr1 = dt.NewRow();
                dr1["Item1"] = "每客早餐成本";
                dr1["MonthActual"] = 0.00;
                dr1["MonthBudget"] = 0.00;
                dr1["ProcessPercent"] = 0.00;
                dr1["LastYear"] = 0.00;
                dr1["YoY"] = 0.00;
                dr1["YearAct"] = 0.00;
                dr1["AccuBudget"] = 0.00;
                dr1["FinishPercent"] = 0.00;
                dr1["LastYear_Bud"] = 0.00;
                dr1["YoY_Bud"] = 0.00;
                dr1["Orderid"] = 31;
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }


        public decimal getEfficiencyBudget(DataTable mTable, string mAccount,string HotelId, string mYear, string mMonth)
        {
            decimal mReturnValue = 0.00m;

            int v_resturantId = int.Parse(HotelId);
            string v_account = mAccount;

            var query =
               from T in mTable.AsEnumerable()
               where T.IsNull(v_account) == false
               select new
               {
                   Value_Account = T.Field<decimal>(v_account)
               };

            foreach (var b in query)
            {
                mReturnValue = b.Value_Account;
            }

            return mReturnValue;
        }

        public decimal getEfficiencyMonthValue(DataTable mTable, string mAccount, string mHotelId, string mYear, string mMonth)
        {

            decimal mReturnValue = 0.00m;
            string v_account = mAccount;

            var query =
               from T in mTable.AsEnumerable()
               where T.IsNull(v_account) == false
               select new
               {
                   Value_Account = T.Field<decimal>(v_account)
               };

            foreach (var b in query)
            {
                mReturnValue = b.Value_Account;
            }

            return mReturnValue;
        }

        public decimal getOEValue(DataTable mTable, string mAccount)
        {
            decimal mReturnValue = 0.00m;


            if (mTable.Rows.Count > 0)
            {
                if (mTable.Rows[0][mAccount] != System.DBNull.Value )
                {
                    mReturnValue = decimal.Parse(((DataRow)mTable.Rows[0])[mAccount].ToString());
                }
            }

            return mReturnValue;

        }


        //get restaurant report
        public DataSet getRestaurantReportDS(string mHotelId, string mYear, string mMonth)
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

            if (mHotelId == "1")
            {
                SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[MockName1]
                      ,[MockName2]
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
                  FROM [Repository].[dbo].[vwRestaurantReport1]
                  where yearCode={0} and monthCode={1}
                    ", mYear, mMonth);
            }

            if (mHotelId == "2")
            {
                SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[MockName1]
                      ,[MockName2]
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
                  FROM [Repository].[dbo].[vwRestaurantReport2]
                  where yearCode={0} and monthCode={1}
                    ", mYear, mMonth);
            }

            if (mHotelId == "3")
            {
                SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[MockName1]
                      ,[MockName2]
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
                  FROM [Repository].[dbo].[vwRestaurantReport3]
                  where yearCode={0} and monthCode={1}
                    ", mYear, mMonth);
            }


            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
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
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }

        public DataSet getRestaurantEfficiency2(string HotelId, string mYear, string mMonth)
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

            string SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[MockName1]
                      ,[MockName2]
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
                  FROM [Repository].[dbo].[vwRestaurantEfficiency1]
                  where yearCode={0} and monthCode={1}
                    ", mYear, mMonth);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
            DataRow dr1 = null;

            foreach (DataRow row in ds1.Tables[0].Rows)
            {
                dr1 = dt.NewRow();
                dr1["Item1"] = row["MockName1"].ToString();
                dr1["Item2"] = row["MockName1"].ToString();

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



        public DataSet getRestaurantMainReportDS(string mHotelId, string mYear, string mMonth)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string[] fields = new string[] { "Item1", "Item2", "MonthActual", "MonthBudget", "ProcessPercent", "LastMonthActual", "QoQ", "LastYear", "YoY", "YearAct", "SameYearBudget", "YearPercent", "QoQYear", "YoYYear", "YearBudget", "YearBudgetPercent", "Orderid","ItemId" };
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

            if (mHotelId == "1")
            {
                SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[MockName1]
                      ,[MockName2]
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
                  FROM [Repository].[dbo].[vwRestaurantMainReport1]
                  where yearCode={0} and monthCode={1} order by Sequence
                    ", mYear, mMonth);

            }



            if (mHotelId == "2")
            {
                SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[MockName1]
                      ,[MockName2]
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
                  FROM [Repository].[dbo].[vwRestaurantMainReport2]
                  where yearCode={0} and monthCode={1} or yearCode is null order by Sequence
                    ", mYear, mMonth);

            }


            if (mHotelId == "3")
            {
                SQL_STATEMENT = string.Format(@"
                SELECT [ReportId]
                      ,[ReportName]
                      ,[ChineseName]
                      ,[HotelId]
                      ,[Sequence]
                      ,[ItemId]
                      ,[MockName1]
                      ,[MockName2]
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
                  FROM [Repository].[dbo].[vwRestaurantMainReport3]
                  where yearCode={0} and monthCode={1} order by Sequence
                    ", mYear, mMonth);

            }


//            SQL_STATEMENT = string.Format(@"
//                SELECT [ReportId]
//                      ,[ReportName]
//                      ,[ChineseName]
//                      ,[HotelId]
//                      ,[Sequence]
//                      ,[ItemId]
//                      ,[MockName1]
//                      ,[MockName2]
//                      ,[AccountType]
//                      ,[Department]
//                      ,[yearCode]
//                      ,[MonthCode]
//                      ,[本月实际]
//                      ,[本月预算]
//                      ,[本月完成比例]
//                      ,[上月实际]
//                      ,[本月环比增减]
//                      ,[上年本月同期]
//                      ,[本月同比增减]
//                      ,[本年实际]
//                      ,[同期预算]
//                      ,[本年完成比例]
//                      ,[上年本年累计同期]
//                      ,[本年同比增减]
//                      ,[全年预算]
//                      ,[全年完成比例]
//                  FROM [Repository].[dbo].[vwRestaurantMainReport1]
//                  where yearCode={0} and monthCode={1}
//                    ", mYear, mMonth);


            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);
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
                dt.Rows.Add(dr1);
            }

            ds.Tables.Add(dt);
            return ds;
        }

    }
}
