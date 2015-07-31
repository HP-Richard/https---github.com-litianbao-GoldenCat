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
    public partial class Role
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public RoleStatuses Status { get; set; }
    }
}
