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
    public partial class UnallocateReportComponent
    {

        public DataSet GetUnallocateReport(string mHotelId, string mYear, string mMonth)
        {
            var UnallocateDAC = new UnallocateReportDAL();
            return UnallocateDAC.getUnallocateReportDS(mHotelId, mYear, mMonth);
        }

    }
}
