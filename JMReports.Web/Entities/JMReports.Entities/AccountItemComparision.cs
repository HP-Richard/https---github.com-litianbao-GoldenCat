using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;


namespace JMReports.Entities
{
    /// <summary>
    /// Represents a row of AccountItemComparision.
    /// </summary>
    [DataContract]
    public partial class AccountItemComparision
    {

        /// <summary>
        /// Gets or sets a long value for the AccountID column.
        /// </summary>
        [DataMember]
        public int ItemID { get; set; }

        /// <summary>
        /// Gets or sets a int value for the ItemID column.
        /// </summary>
        [DataMember]
        public int Divisor { get; set; }

        /// <summary>
        /// Gets or sets a int value for the Divisor column.
        /// </summary>
        [DataMember]
        public int Dividend { get; set; }

        /// <summary>
        /// Gets or sets a int value for the Dividend column.
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a string value for the Comment column.
        /// </summary>
        [DataMember]
        public int YearCode { get; set; }

        
    }
}
