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
    public class HotelAnalysisComponent
    {

        public DataSet getHotelAnalysisReportDS(string mHotelId, string mYear, string mItemId)
        {
            var HotelAnalysisDAC = new HotelAnalysisDAL();
            return HotelAnalysisDAC.getHotelAnalysisReportDS(mHotelId, mYear, mItemId);
        }


        public DataSet getHotelAnalysis2ReportDS(string mHotelId, string mYear, string mItemId)
        {
            var HotelAnalysisDAC = new HotelAnalysisDAL();
            return HotelAnalysisDAC.getHotelAnalysis2ReportDS(mHotelId, mYear, mItemId);
        }
    }
}
