using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using JMReports.WebApp.Utility;

namespace JMReports.WebApp.ReportBussiness
{
    public class HotelAnalysisReport
    {
        public System.Data.DataSet getAnalysisReport(string mHotelId, string mYear, string mItemId)
        {
            if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
            {
                var cacheds = Helper.GetCacheData(mHotelId, mYear, mItemId);
                if (cacheds == null)
                {
                    JMReports.Business.HotelAnalysisComponent hc = new Business.HotelAnalysisComponent();
                    var ds = hc.getHotelAnalysisReportDS(mHotelId, mYear, mItemId);
                    ds.AddCacheData(mHotelId, mYear, mItemId);

                }
                return Helper.GetCacheData(mHotelId, mYear, mItemId);
            }
            else
            {
                JMReports.Business.HotelAnalysisComponent hc = new Business.HotelAnalysisComponent();
                return hc.getHotelAnalysisReportDS(mHotelId, mYear, mItemId);
            }
        }


        public System.Data.DataSet getAnalysis2Report(string mHotelId, string mYear, string mItemId)
        {
            if (Helper.GetAppSetting<bool>(CONSTANT.USINGCACHESETTINGKEY))
            {
                var cacheds = Helper.GetCacheData(mHotelId, mYear, mItemId);
                if (cacheds == null)
                {
                    JMReports.Business.HotelAnalysisComponent hc = new Business.HotelAnalysisComponent();
                    var ds = hc.getHotelAnalysis2ReportDS(mHotelId, mYear, mItemId);
                    ds.AddCacheData(mHotelId, mYear, mItemId);

                }
                return Helper.GetCacheData(mHotelId, mYear, mItemId);
            }
            else
            {
                JMReports.Business.HotelAnalysisComponent hc = new Business.HotelAnalysisComponent();
                return hc.getHotelAnalysis2ReportDS(mHotelId, mYear, mItemId);
            }
        }

    }
}