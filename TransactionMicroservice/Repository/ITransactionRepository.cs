using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionMicroservice.Repository
{
    public interface ITransactionRepository
    {
        public transactionMessage deposit(transactionInput value);
        public transactionMessage withdraw(transactionInput value);
        public transactionMessage transfer(transfers value);
    }
}
