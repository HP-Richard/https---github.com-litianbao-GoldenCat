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
    public partial class RoomMarketReportComponent
    {

        public DataSet GetRoomMarketReport(string mHotelId, string mYear, string mMonth)
        {

            var RoomMarketDAC = new RoomMarketReportDAL();
            return RoomMarketDAC.getRoomMarketReportDS(mHotelId, mYear, mMonth);
        }

        public DataSet GetRoomMarketReport2(string mHotelId, string mYear, string mMonth)
        {

            var RoomMarketDAC = new RoomMarketReportDAL();
            return RoomMarketDAC.getRoomMarketReportDS2(mHotelId, mYear, mMonth);
        }

        public DataSet GetRoomMarketCompanyPriceDS(string mHotelId, string mYear, string mMonth)
        {
            var RoomMarketDAC = new RoomMarketReportDAL();
            return RoomMarketDAC.getRoomMarketCompanyPriceDS(mHotelId, mYear, mMonth);
        }

        public DataSet GetRoomMarketGroupDetailDS(string mHotelId, string mYear, string mMonth)
        {
            var RoomMarketDAC = new RoomMarketReportDAL();
            return RoomMarketDAC.getRoomMarketGroupDetail(mHotelId, mYear, mMonth);
        }
    }
}
