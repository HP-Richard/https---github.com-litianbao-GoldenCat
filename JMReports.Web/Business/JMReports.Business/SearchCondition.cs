using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMReports.Business
{
    [Serializable()]
    public class SearchCondition
    {
        public string Hotel { get; set; }
        public string YearCode { get; set; }

        public string MonthCode { get; set; }

        public string ItemId { get; set; }
    }
}
