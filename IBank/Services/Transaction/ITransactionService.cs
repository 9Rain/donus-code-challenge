using IBank.Dtos.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBank.Services.Transaction
{
    public interface ITransactionService
    {
        Task<IEnumerable<ReturnListTransactionDto>> List(long id, ListTransactionDto range);
        Task<ReturnTransactionDto> Deposit(DepositTransactionDto deposit);
        Task<ReturnTransactionDto> Withdraw(long clientId, WithdrawTransactionDto withdraw);
        Task<ReturnTransactionDto> Transfer(long clientId, TransferTransactionDto transfer);  
    }
}
