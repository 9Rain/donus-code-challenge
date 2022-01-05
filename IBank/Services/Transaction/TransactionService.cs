using AutoMapper;
using DataAccess.Data.Account;
using DataAccess.Data.Transaction;
using IBank.Dtos.Transaction;
using IBank.Exceptions;
using IBank.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBank.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionData _transactionData;
        private readonly IAccountData _accountData;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public TransactionService(
            ITransactionData transactionData, 
            IAccountData accountData,
            IAccountService accountService,
            IMapper mapper
        )
        {
            _transactionData = transactionData;
            _accountData = accountData;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReturnListTransactionDto>> List(long id, ListTransactionDto range)
        {
            var list = await _transactionData.List(id, range.StartDate, range.EndDate);
            return list.Select(t => {
                var obj = _mapper.Map<ReturnListTransactionDto>(t);

                if (t.IsIncome && t.To?.ClientId == id)
                {
                    obj.Type = "Income";
                }
                else
                {
                    obj.Type = "Outcome";
                }

                return obj;
            });
        }

        public async Task<ReturnTransactionDto> Deposit(DepositTransactionDto deposit)
        {
            var account = await _accountService.GetForTransaction(deposit.Addresse);
            var referenceId = await this.GenerateReferenceId();
            var transaction = await _transactionData.Deposit(account, referenceId, deposit.Amount);

            return _mapper.Map<ReturnTransactionDto>(transaction);
        }

        public async Task<ReturnTransactionDto> Transfer(long clientId, TransferTransactionDto transfer)
        {
            var origin = await _accountData.GetByClientId(clientId);
            
            if (origin.Balance < transfer.Amount)
            {
                throw new InsufficientBalanceException();
            }

            var destination = await _accountService.GetForTransaction(transfer.Addresse);

            if(destination.ClientId == clientId)
            {
                throw new InvalidOperationException("Origin and destination account can't be the same");
            }

            var referenceId = await this.GenerateReferenceId();
            var transaction = await _transactionData.Transfer(origin, destination, referenceId, transfer.Amount);

            return _mapper.Map<ReturnTransactionDto>(transaction);
        }

        public async Task<ReturnTransactionDto> Withdraw(long clientId, WithdrawTransactionDto withdraw)
        {
            var account = await _accountData.GetByClientId(clientId);
            if(account.Balance < withdraw.Amount)
            {
                throw new InsufficientBalanceException();
            }

            var referenceId = await this.GenerateReferenceId();
            var transaction = await _transactionData.Withdraw(account, referenceId, withdraw.Amount);

            return _mapper.Map<ReturnTransactionDto>(transaction);
        }

        private async Task<string> GenerateReferenceId()
        {
            var rand = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string referenceId;

            do
            {
                referenceId = new string(Enumerable.Repeat(chars, 100)
                    .Select(s => s[rand.Next(s.Length)]).ToArray());
            }
            while (await _transactionData.GetByReferenceId(referenceId) != null);

            return referenceId;
        }
    }

}
