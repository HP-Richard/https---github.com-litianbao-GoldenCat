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
    public partial class BanquetMarketReportComponent
    {
        public DataSet GetBanquetMarketReport(string mHotelId, string mYear, string mMonth)
        {
            var BanquetMarketDAC = new BanquetMarketReportDAL();
            return BanquetMarketDAC.getBanquetMarketReportDS(mHotelId, mYear, mMonth);
        }
    }
}
