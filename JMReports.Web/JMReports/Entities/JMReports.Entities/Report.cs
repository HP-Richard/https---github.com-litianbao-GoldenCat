using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace JMReports.Entities
{
    [DataContract]
    public partial class Report
    {
        [DataMember]
        public int ReportId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ChineseName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string URL { get; set; }

    }
}
