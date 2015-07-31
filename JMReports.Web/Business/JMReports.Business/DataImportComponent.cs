using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JMReports.Entities;
using JMReports.Data;
using System.Transactions;
using System.Linq;
using System.Data;

namespace JMReports.Business
{
    public class DataImportComponent
    {
        public IList<ErrorMessage> Errors { get; private set; }
        public DataSet GetDailyReport(DateTime dt1, string hotel)
        {

            var DailyReportDAC = new DailyReportDAL();

            return DailyReportDAC.getDailyReportDS(dt1, hotel);

        }

        public bool LogDataImportStatus(int datatype, int yearcode, int hotelid, string message)
        {
            //var DataImportDAC = new DataImportDAL();
            //return DataImportDAL.

            var DataImportDAC = new DataImportDAL();
            var retBool = DataImportDAC.LogDataImportStatus(datatype, yearcode, hotelid, message);

            return retBool;
        }

        public int DataImport(DataTable dt, string tablename, int yearcode, int monthcode, int hotelid)
        {
            var DataImportDAC = new DataImportDAL();
            int retInt = DataImportDAC.DataImport(dt, tablename, yearcode, monthcode, hotelid);

            Errors = DataImportDAC.Errors;

            return retInt;
 
        }


        public int DataImportBudget(DataTable dt, string tablename, int yearcode, int hotelid)
        {
            var DataImportDAC = new DataImportDAL();
            return DataImportDAC.DataImportBudget(dt, tablename, yearcode, hotelid);

        }
    }
}
