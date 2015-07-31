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
    public partial class ReportItemDAL : DataAccessComponent
    {

        public DataTable getReportItems()
        {

            string SQL_STATEMENT = string.Empty;
            SQL_STATEMENT = string.Format(@"
                select ID,AccountType,Department,LTRIM(STR(ID)) + ' | ' + AccountType + ' | ' +Department as ItemName 
                from ReportItem 
                order by ID
            ");


            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            return ds1.Tables[0];

        }


        public DataTable getAnalysisItems()
        {

            string SQL_STATEMENT = string.Empty;
            SQL_STATEMENT = string.Format(@"
                select ID,MockName as ItemName  
                from ReportItem 
                order by ID
            ");


            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            DataSet ds1 = db.ExecuteDataSet(CommandType.Text, SQL_STATEMENT);

            return ds1.Tables[0];

        }


    }
}
