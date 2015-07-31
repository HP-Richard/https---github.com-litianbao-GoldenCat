using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JMReports.Entities;

namespace JMReports.Data
{
    public class HotelAnalysisDAL : DataAccessComponent
    {
        public DataSet getHotelAnalysisReportDS(string mHotelId, string mYear, string mItemId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string SQL_STATEMENT = string.Empty;


            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DbCommand dbcmd = db.GetStoredProcCommand("sp_HotelAnalysis_month");

            db.AddInParameter(dbcmd, "HotelId", DbType.Int32,int.Parse(mHotelId)); 
            db.AddInParameter(dbcmd, "YearCode", DbType.Int32,int.Parse(mYear)); 
            db.AddInParameter(dbcmd, "ItemId", DbType.Int32,int.Parse(mItemId)); 


             ds = db.ExecuteDataSet(dbcmd);
 
            return ds;
        }

        public DataSet getHotelAnalysis2ReportDS(string mHotelId, string mYear, string mItemId)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string SQL_STATEMENT = string.Empty;


            SQL_STATEMENT = string.Format(@"
		        select M.yearcode OnYear ,M.monthcode OnMonth,M.itemid ItemId,M.Act ActValue,R.MockName ItemName		        from ActMonthly2 M		        left join ReportItem R on M.ItemId=R.ID		        where  M.yearCode={0} and M.ItemId={1} and M.HotelId={2}	   
            ", mYear,mItemId,mHotelId);

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);


            ds = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT); 

            return ds;
        }

    }
}
