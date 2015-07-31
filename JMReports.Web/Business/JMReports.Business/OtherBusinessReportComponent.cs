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
    public partial class OtherBusinessReportComponent
    {

        public DataSet GetOtherBusinessReport(string mHotelId, string mYear, string mMonth)
        {

            OtherBusinessReportDAL otherBusinessDAC = new OtherBusinessReportDAL();

            return otherBusinessDAC.getOtherBusinessReportDS(mHotelId, mYear, mMonth);
        }
    }
}
