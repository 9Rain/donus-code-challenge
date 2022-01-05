using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data.Transaction
{
    public interface ITransactionData
    {
        Task<TransactionModel?> GetByReferenceId(string referenceId);
        Task<IEnumerable<TransactionModel>> List(long clientId, DateTime startDate, DateTime endDate);
        Task<TransactionModel> Deposit(AccountModel account, string referenceId, decimal amount);
        Task<TransactionModel> Withdraw(AccountModel account, string referenceId, decimal amount);
        Task<TransactionModel> Transfer(AccountModel origin, AccountModel destination, string referenceId, decimal amount);
    }
}
