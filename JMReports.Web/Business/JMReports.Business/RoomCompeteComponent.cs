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
    public partial class  RoomCompeteComponent
    {
        public DataSet getCombinationDS(string HotelId, string mYear, string mMonth)
        {

            RoomCompeteDAL RoomCompeteDAC = new RoomCompeteDAL();

            return RoomCompeteDAC.getCombinationDS(HotelId, mYear, mMonth);
        }

        public DataSet getRoomCompeteCurrentMonthDS(string HotelId, string mYear, string mMonth, string combination)
        {

            RoomCompeteDAL RoomCompeteDAC = new RoomCompeteDAL();

            return RoomCompeteDAC.getRoomCompeteCurrentMonthDS(HotelId, mYear, mMonth, combination);
        }

        public DataSet GetRoomCompeteDS(string HotelId, string mYear, string mMonth)
        {

            RoomCompeteDAL RoomCompeteDAC = new RoomCompeteDAL();

            return RoomCompeteDAC.getRoomCompeteDS(HotelId, mYear, mMonth);
        }

        public DataSet GetRoomCompeteDS2(string HotelId, string mYear, string mMonth, string combination)
        {

            RoomCompeteDAL RoomCompeteDAC = new RoomCompeteDAL();

            return RoomCompeteDAC.getRoomCompeteDS2(HotelId, mYear, mMonth, combination);
        }
    }
}
