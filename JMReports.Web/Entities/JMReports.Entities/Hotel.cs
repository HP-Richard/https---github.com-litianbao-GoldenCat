using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JMReports.Entities
{
    [DataContract]
    public partial class Hotel
    {
        [DataMember]
        public int HotelId { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ChineseName { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public string ManagementGroupName { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public DateTime OpenDate { get; set; }

        [DataMember]
        public int RoomCount { get; set; }

        [DataMember]
        public int RestaurantsCount { get; set; }

        [DataMember]
        public int BarsCount { get; set; }

        [DataMember]
        public decimal BanquetHallArea { get; set; }

        [DataMember]
        public decimal SpaArea { get; set; }
    }
}
