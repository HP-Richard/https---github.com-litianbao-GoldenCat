using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace JMReports.WebApp.ReportBussiness
{
    public class RoomEfficiencyReport
    {
        public System.Data.DataSet getRoomEfficiency(string HotelId, string mYear, string mMonth)
        {
            JMReports.Business.RoomEfficiencyComponent rc = new Business.RoomEfficiencyComponent();
            return rc.getRoomEfficiencyDS(HotelId, mYear, mMonth); 
        }
    }
}