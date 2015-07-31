using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JMReports.Entities;
using JMReports.Data;
using System.Transactions;
using System.Linq;

namespace JMReports.Business
{
    /// <summary>
    /// Account business component.
    /// </summary>
    public partial class AccountComponent
    {
        /// <summary>
        /// Apply business method. 
        /// </summary>
        /// <param name="Account">A Account value.</param>
        /// <returns>Returns a Account object.</returns>
        public Account Apply(Account Account)
        {
            Account.Status = AccountStatuses.Pending;
            Account.DateSubmitted = DateTime.Now;
            Account.IsCompleted = false;

            AccountStatusLog log = CreateLog(Account);

            // Data access component declarations.
            var AccountDAC = new AccountDAL();
            var AccountStatusLogDAC = new AccountStatusLogDAL();

            // Check for overlapping Accounts.
            if (AccountDAC.IsOverlap(Account))
            {
                throw new ApplicationException("Date range is overlapping with another Account.");
            }

            using (TransactionScope ts =
                new TransactionScope(TransactionScopeOption.Required))
            {
                // Step 1 - Calling Create on AccountDAC.
                AccountDAC.Create(Account);

                // Step 2 - Calling Create on AccountStatusLogDAC.
                log.AccountID = Account.AccountID;
                AccountStatusLogDAC.Create(log);

                ts.Complete();
            }

            return Account;
        }

        /// <summary>
        /// Cancel business method. 
        /// </summary>
        /// <param name="Account">A Account value.</param>
        /// <returns>Returns a Account object.</returns>
        public Account Cancel(Account Account)
        {
            Account.Status = AccountStatuses.Cancelled;
            Account.IsCompleted = true;

            UpdateStatus(Account);

            return Account;
        }

        /// <summary>
        /// Approve business method. 
        /// </summary>
        /// <param name="Account">A Account value.</param>
        /// <returns>Returns a Account object.</returns>
        public Account Approve(Account Account)
        {
            Account.Status = AccountStatuses.Approved;
            Account.IsCompleted = true;

            UpdateStatus(Account);

            return Account;
        }

        /// <summary>
        /// Reject business method. 
        /// </summary>
        /// <param name="Account">A Account value.</param>
        /// <returns>Returns a Account object.</returns>
        public Account Reject(Account Account)
        {
            Account.Status = AccountStatuses.Rejected;
            Account.IsCompleted = true;

            UpdateStatus(Account);

            return Account;
        }

        private static AccountStatusLog CreateLog(Account Account)
        {
            AccountStatusLog log = new AccountStatusLog();
            log.Date = DateTime.Now;
            log.AccountID = Account.AccountID;
            log.Status = Account.Status;
            return log;
        }

        private void UpdateStatus(Account Account)
        {
            AccountStatusLog log = CreateLog(Account);

            // Data access component declarations.
            var AccountDAC = new AccountDAL();
            var AccountStatusLogDAC = new AccountStatusLogDAL();

            using (TransactionScope ts =
                new TransactionScope(TransactionScopeOption.Required))
            {
                // Step 1 - Calling UpdateById on AccountDAC.
                AccountDAC.UpdateStatus(Account);

                // Step 2 - Calling Create on AccountStatusLogDAC.
                AccountStatusLogDAC.Create(log);

                ts.Complete();
            }
        }

        /// <summary>
        /// ListAccountsByEmployee business method. 
        /// </summary>
        /// <param name="startRowIndex">A startRowIndex value.</param>
        /// <param name="maximumRows">A maximumRows value.</param>
        /// <param name="sortExpression">A sortExpression value.</param>
        /// <param name="employee">A employee value.</param>
        /// <param name="category">A category value.</param>
        /// <param name="status">A status value.</param>
        /// <returns>Returns a List<Account> object.</returns>
        public List<Account> ListAccountsByEmployee(int maximumRows, int startRowIndex, 
            string sortExpression, string employee, AccountCategories? category, AccountStatuses? status,
            out int totalRowCount)
        {
            List<Account> result = default(List<Account>);

            if (string.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "DateSubmitted DESC";

            // Data access component declarations.
            var AccountDAC = new AccountDAL();

            // Step 1 - Calling Select on AccountDAC.
            result = AccountDAC.Select(maximumRows, startRowIndex, sortExpression,
                employee, category, status);

            // Step 2 - Get count.
            totalRowCount = AccountDAC.Count(employee, category, status);

            return result;

        }

        /// <summary>
        /// ListLogsByAccount business method. 
        /// </summary>
        /// <param name="AccountID">A AccountID value.</param>
        /// <returns>Returns a List<AccountStatusLog> object.</returns>
        public List<AccountStatusLog> ListLogsByAccount(long AccountID)
        {
            // Data access component declarations.
            AccountStatusLogDAL AccountStatusLogDAC = new AccountStatusLogDAL();

            // Step 1 - Calling SelectByAccount on AccountStatusLogDAC.
            List<AccountStatusLog> result = AccountStatusLogDAC.SelectByAccount(AccountID);
            return result;

        }


        /// <summary>
        /// GetAccountById business method. 
        /// </summary>
        /// <param name="AccountID">A AccountID value.</param>
        /// <returns>Returns a Account object.</returns>
        public Account GetAccountById(long AccountID)
        {
            Account result = default(Account);

            // Data access component declarations.
            var AccountDAC = new AccountDAL();

            // Step 1 - Calling SelectById on AccountDAC.
            result = AccountDAC.SelectById(AccountID);
            return result;

        }


        
    }
}
