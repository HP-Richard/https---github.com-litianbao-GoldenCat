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
    public partial class FormulaManageComponent
    {

        public DataSet getForecastReport(string HotelId, string mYear, string mMonth)
        {
            var ForecastDAC = new ForecastDAL();

            return ForecastDAC.getForecastReportDS(HotelId, mYear, mMonth);

        }

        public DataSet getFormula(string mYearCode, string mHotelId, string mAccountItemId)
        {
            var Formula = new FormulaManageDAL();

            return Formula.getFormula(mYearCode, mHotelId, mAccountItemId);
 
        }
    }
}
