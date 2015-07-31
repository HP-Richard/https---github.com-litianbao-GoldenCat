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
    public partial class SpecialReportComponent
    {

        public DataSet GetSpecialReport(string mHotelId, string mYear, string mMonth)
        {
            var SpecialDAC = new SpecialReportDAL();
            return SpecialDAC.getSpecialReportDS( mHotelId, mYear,  mMonth);  
        }
    }
}
