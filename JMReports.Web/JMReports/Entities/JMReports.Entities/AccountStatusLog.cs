using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace JMReports.Entities
{
    /// <summary>
    /// Represents a row of AccountStatusLog data.
    /// </summary>
    [DataContract]
    public partial class AccountStatusLog
    {
        /// <summary>
        /// Gets or sets a long value for the LogID column.
        /// </summary>
        [DataMember]
        [Browsable(false)]
        public long LogID { get; set; }

        /// <summary>
        /// Gets or sets a long value for the AccountID column.
        /// </summary>
        [DataMember]
        public long AccountID { get; set; }

        /// <summary>
        /// Gets or sets a byte value for the Status column.
        /// </summary>
        [DataMember]
        public AccountStatuses Status { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the Date column.
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }
    }
}
