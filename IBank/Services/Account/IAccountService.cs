using System.Threading.Tasks;
using DataAccess.Models;
using IBank.Dtos.Account;
using IBank.Dtos.Transaction.Addresse;

namespace IBank.Services.Account
{
    public interface IAccountService
    {
        Task<ReturnAccountDto> GetByClientId(long clientId);
        Task<ReturnAccountBalanceDto> GetBalance(long clientId);
        Task<AccountModel> GetForTransaction(AddresseTransactionDto addresse);
        Task<ReturnAccountDto> Create(CreateAccountDto account);
        Task DeleteByClientId(long clientId);
    }
}