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
    public partial class HotelWholeReportComponent
    {

        public DataSet GetHotelWholeReport(string mHotelId, string mYear, string mMonth)
        {
            var HotelWholeDAC = new HotelWholeDAL();
            return HotelWholeDAC.getHotelWholeReportDS(mHotelId, mYear, mMonth);
        }
    }
}
