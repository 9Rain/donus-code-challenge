using Dapper;
using DataAccess.Connection;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data.Account
{
    public class AccountData : DataManager, IAccountData
    {
        private readonly IConnection _connection;

        public AccountData(IConnection connection): base(connection)
        {
            _connection = connection;
        }

        public async Task<AccountModel> GetByClientId(long clientId)
        {
            var results = await this.LoadData<dynamic>(
                "dbo.spAccount_GetByClientId", new { ClientId = clientId }
            );

            return results.FirstOrDefault();
        }

        public async Task<AccountModel> GetForTransaction(ClientModel client)
        {
            var parameters = new
            {
                client.Name,
                client.Cpf,
                AgencyNumber = client.Account.Agency.Number,
                AgencyDigit = client.Account.Agency.Digit,
                AccountNumber = client.Account.Number,
                AccountDigit = client.Account.Digit,
            };

            var results = await this.LoadData<dynamic>(
                "dbo.spAccount_GetForTransaction", 
                parameters
            );

            return results.FirstOrDefault();
        }

        public async Task<AccountModel> GetByNumber(string number)
        {
            var results = await this.LoadData<dynamic>(
                "dbo.spAccount_GetByNumber", new { Number = number }
            );

            return results.FirstOrDefault();
        }

        public async Task<Decimal?> GetBalance(long clientId)
        {
            var result = await this.LoadData<Decimal?, dynamic>(
                "dbo.spAccount_GetBalance", new { ClientId = clientId });

            return result.FirstOrDefault();
        }

        public async Task<long> Create(AccountModel account)
        {
            var parameters = new
            {
                AgencyId = account.Agency.Id,
                account.Number,
                account.Digit,
                account.ShortPassword,
                account.Password,
                account.ClientId
            };

            return await this.SaveDataAndReturn<long, dynamic>("dbo.spAccount_Create", parameters);
        }

        private async Task<IEnumerable<AccountModel>> LoadData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = _connection.GetConnection())
            {
                return await connection.QueryAsync<AccountModel, AgencyModel, AccountModel>(
                    storedProcedure,
                    (account, agency) => { account.Agency = agency; return account; },
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
