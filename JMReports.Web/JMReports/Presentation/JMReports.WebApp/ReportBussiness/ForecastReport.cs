using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JMReports.WebApp.ReportBussiness
{
    public class ForecastReport
    {
        public System.Data.DataSet GetForecastReport(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.ForecastComponent fc = new Business.ForecastComponent();
            return fc.getForecastReport(HotelId, mYear, mMonth);
        }

    }
}