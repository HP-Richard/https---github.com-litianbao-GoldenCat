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
    public partial class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int RoleId { get; set; }

        [DataMember]
        public string  RoleName { get; set; }

        [DataMember]
        public string Title { get; set; }
            
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Psd { get; set; }

        [DataMember]
        public UserStatus Status { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public DateTime CreateTime { get; set; }

    }
}
