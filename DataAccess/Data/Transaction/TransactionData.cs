using Dapper;
using DataAccess.Connection;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data.Transaction
{
    public class TransactionData : DataManager, ITransactionData
    {
        private readonly IConnection _connection;

        public TransactionData(IConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<TransactionModel?> GetByReferenceId(string referenceId)
        {
            var result = await this.LoadData<dynamic>(
                "dbo.spTransaction_GetByReferenceId", new { ReferenceId = referenceId });

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<TransactionModel>> List(long clientId, DateTime startDate, DateTime endDate)
        {
            var param = new
            {
                ClientId = clientId,
                StartDate = startDate,
                EndDate = endDate
            };

            return await this.LoadData<dynamic>("dbo.spTransaction_List", param);
        }

        public async Task<TransactionModel> Deposit(AccountModel account, string referenceId, decimal amount)
        {
            var param = new 
            { 
                Amount = amount,
                ReferenceId = referenceId,
                DesiredCompletionDate = DateTime.Now,
                CompletedAt = DateTime.Now,
                IsCompleted = 1,
                ToAccountId = account.Id,
                AccountNewBalance = account.Balance + amount
            };

            return await this.SaveDataAndReturn<TransactionModel, dynamic>("dbo.spTransaction_Deposit", param);
        }

        public async Task<TransactionModel> Withdraw(AccountModel account, string referenceId, decimal amount)
        {
            var param = new
            {
                Amount = amount,
                ReferenceId = referenceId,
                DesiredCompletionDate = DateTime.Now,
                CompletedAt = DateTime.Now,
                IsCompleted = 1,
                FromAccountId = account.Id,
                AccountNewBalance = account.Balance - amount
            };

            return await this.SaveDataAndReturn<TransactionModel, dynamic>("dbo.spTransaction_Withdraw", param);
        }

        public async Task<TransactionModel> Transfer(AccountModel origin, AccountModel destination, string referenceId, decimal amount)
        {
            var param = new
            {
                Amount = amount,
                ReferenceId = referenceId,
                DesiredCompletionDate = DateTime.Now,
                CompletedAt = DateTime.Now,
                IsCompleted = 1,
                FromAccountId = origin.Id,
                ToAccountId = destination.Id,
                OriginNewBalance = origin.Balance - amount,
                DestinationNewBalance = destination.Balance + amount
            };

            return await this.SaveDataAndReturn<TransactionModel, dynamic>("dbo.spTransaction_Transfer", param);
        }

        private async Task<IEnumerable<TransactionModel>> LoadData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = _connection.GetConnection())
            {
                return await connection.QueryAsync<
                    TransactionModel, TransactionActionModel, 
                    AccountModel, AccountModel, TransactionModel>(
                        storedProcedure,
                        (transaction, action, from, to) => this.MapResults(transaction, action, from, to),
                        parameters,
                        commandType: CommandType.StoredProcedure
                );
            }
        }

        private TransactionModel MapResults(
            TransactionModel transaction,
            TransactionActionModel action,
            AccountModel from,
            AccountModel to
        )
        {
            transaction.Action = action;
            transaction.From = from;
            transaction.To = to;
            return transaction;
        }
    }
}
