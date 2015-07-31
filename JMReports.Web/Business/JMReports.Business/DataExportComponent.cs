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
    public class DataExportComponent
    {
        public IList<ErrorMessage> Errors { get; private set; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        /// <returns></returns>
        public int DataExport(string mTitle, string mHotelid, string mYear, string mMonth, string tablenames)
        {

            var DataExportDAC = new DataExportDAL();
            int returnVal = 0;

            try
            {
                DataExportDAC.ExportData(mTitle, mHotelid, mYear, mMonth, tablenames);
                returnVal = 1;
            }
            catch (Exception ex)
            {
                returnVal = 0;
                throw ex;
            }


            return returnVal;
 
        }
    }
}
