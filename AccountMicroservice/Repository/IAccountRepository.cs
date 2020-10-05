using AccountMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Repository
{
    public interface IAccountRepository
    {
        public List<currentAccountDetails> getCurrent();
        public List<savingsAccount> getSavings();
        public List<accountDetails> getCustomerAccounts(int id);
        public customerAccount createAccount(int id);
        public accountDetails getAccount(int id);
        public IEnumerable<statement> getAccountStatement(int accountId, int fromDate, int toDate);
        public accountDetails deposit(transactionInput value);
        public accountDetails withdraw(transactionInput value);
        public transactionStatusDetails transfer(transfers value);

    }
}
