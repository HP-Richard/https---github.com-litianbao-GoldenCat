using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JMReports.Entities;
using JMReports.Data;
using System.Transactions;
using System.Linq;

namespace JMReports.Business
{
    public partial class RoleComponent
    {

        public List<Role> ListRoles(int maximumRows, int startRowIndex, string sortExpression, string roleName, RoleStatuses? status, out int totalRowCount)
        {
            List<Role> result = default(List<Role>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "CreateTime DESC";

            // Data access component declarations.
            var RoleDAC = new RoleDAL(); 

            // Step 1 - Calling Select on AccountDAC.
            result = RoleDAC.Select(maximumRows, startRowIndex, sortExpression, roleName,status);

            // Step 2 - Get count.
            totalRowCount = RoleDAC.Count(roleName, status);

            return result;
        }


        public List<Role> getRoles()
        {
            List<Role> result = default(List<Role>);

            // Data access component declarations.
            var RoleDAC = new RoleDAL();

            result = RoleDAC.getRoles();
            return result;
 
        }

        public void insertRoleReport()
        {
            var RoleDAC = new RoleDAL();
            
        }



        /// <summary>
        /// 设置角色报表，每个角色对应的报表
        /// </summary>
        public int setRoleReport(int RoleId, string strReportId)
        {
            int retVal = 0;

            var RoleDAC = new RoleDAL();

            //删除此角色下的说有报表
            retVal = RoleDAC.DeleteRoleReport(RoleId);

            //增加此角色下的所有报表
            string[] repId = strReportId.Split(',');

            for (int i = 0; i <= repId.Length - 1; i++)
            {
                retVal &= RoleDAC.insertRoleReport(RoleId, int.Parse(repId[i].ToString()));
            }

            return retVal;
        }

        public string  getReportIDs(int RoleId)
        {

            var RoleDAC = new RoleDAL();
            return RoleDAC.getReportIDs(RoleId);

        }
    }
}
