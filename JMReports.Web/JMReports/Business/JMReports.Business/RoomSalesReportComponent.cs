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
    public partial class RoomSalesReportComponent
    {
        public DataSet GetRoomSalesReport(string mHotelId, string mYear, string mMonth)
        {
            var RoomSalesDAC = new RoomSalesReportDAL();
            return RoomSalesDAC.getRoomSalesReportDS(mHotelId, mYear, mMonth);
        }
    }
}
