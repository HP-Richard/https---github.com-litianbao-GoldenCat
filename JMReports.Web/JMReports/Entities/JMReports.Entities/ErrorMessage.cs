using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JMReports.Entities
{
    
    [DataContract]
    public partial class ErrorMessage
    {
        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public string ItemId { get; set; }

        [DataMember]
        public double? LeftBudget { get; set; }

        [DataMember]
        public double? RightBudget { get; set; }

        [DataMember]
        public ErrorType ErrorType { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    public enum ErrorType
    {
        MissingBugdet = 0,
        BugdetMismatch
    }
}
