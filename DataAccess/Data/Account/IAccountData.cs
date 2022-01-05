using DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace DataAccess.Data.Account
{
    public interface IAccountData
    {
        Task<AccountModel> GetByClientId(long clientId);
        Task<AccountModel> GetForTransaction(ClientModel client);
        Task<AccountModel> GetByNumber(string number);
        Task<Decimal?> GetBalance(long clientId);
        Task<long> Create(AccountModel account);
    }
}
