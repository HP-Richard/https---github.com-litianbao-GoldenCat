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
    public class HotelDAL : DataAccessComponent
    {
        public DataSet getHotels()
        {
            DataSet ds = new DataSet();
            string sqlStr = "select HotelId,Code,Name,ChineseName from Hotel";

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            ds = db.ExecuteDataSet(CommandType.Text, sqlStr);
            return ds;

        }
    }
}
