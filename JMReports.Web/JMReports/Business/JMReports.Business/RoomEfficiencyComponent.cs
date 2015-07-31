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
    public partial class RoomEfficiencyComponent
    {

        public DataSet getRoomEfficiencyDS(string HotelId, string mYear, string mMonth)
        {
            RoomEfficiencyDAL roomEfficiencyDAC = new RoomEfficiencyDAL();

            return roomEfficiencyDAC.getRoomEfficiencyDS(HotelId, mYear, mMonth);
        }
    }
}
