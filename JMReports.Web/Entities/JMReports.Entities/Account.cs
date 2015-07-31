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
    /// Represents a row of Account data.
    /// </summary>
    [DataContract]
    public partial class Account
    {

        /// <summary>
        /// Gets or sets a long value for the AccountID column.
        /// </summary>
        [DataMember]
        [Browsable(false)]
        public long AccountID { get; set; }

        /// <summary>
        /// Gets or sets a Guid value for the CorrelationID column.
        /// </summary>
        [DataMember]
        public Guid CorrelationID { get; set; }

        /// <summary>
        /// Gets or sets a byte value for the Category column.
        /// </summary>
        [DataMember]
        public AccountCategories Category { get; set; }

        /// <summary>
        /// Gets or sets a string value for the Employee column.
        /// </summary>
        [DataMember]
        public string Employee { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the StartDate column.
        /// </summary>
        [DataMember]
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the EndDate column.
        /// </summary>
        [DataMember]
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets a string value for the Description column.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a byte value for the Duration column.
        /// </summary>
        [DataMember]
        [Required]
        [Range(1, 90)]
        public byte Duration { get; set; }

        /// <summary>
        /// Gets or sets a byte value for the Status column.
        /// </summary>
        [DataMember]
        public AccountStatuses Status { get; set; }

        /// <summary>
        /// Gets or sets a bool value for the IsCompleted column.
        /// </summary>
        [DataMember]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets a string value for the Remarks column.
        /// </summary>
        [DataMember]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets a DateTime value for the DateSubmitted column.
        /// </summary>
        [DataMember]
        public DateTime DateSubmitted { get; set; }
    }
}
