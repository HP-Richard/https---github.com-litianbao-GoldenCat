using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMReports.Entities
{
    [Serializable]
    public enum AccountStatuses : byte
    {
        Pending,
        Cancelled,
        Approved,
        Rejected
    }

    public enum RoleStatuses : byte
    {
        Unavailable,
        Available

    }

    public enum UserStatus : byte
    {
        不可用,
        可用
    }
}
