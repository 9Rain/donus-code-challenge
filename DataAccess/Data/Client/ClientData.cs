using DataAccess.Models;
using System.Threading.Tasks;
using System.Linq;
using DataAccess.Connection;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace DataAccess.Data.Client
{
    public class ClientData : DataManager, IClientData
    {
        private readonly IConnection _connection;

        public ClientData(IConnection connection) : base(connection)
        {
            _connection = connection;
        }

        public async Task<ClientModel> Get(long id)
        {
            var result = await this.LoadData<dynamic>(
                "dbo.spClient_Get", new { Id = id });

            return result.FirstOrDefault();
        }

        public async Task<bool> HasActiveOrDisabledAccount(string cpf)
        {
            var result = await this.LoadData<bool, dynamic>(
                "dbo.spClient_HasActiveOrDisabledAccount", new { Cpf = cpf });

            return result.FirstOrDefault();
        }

        public async Task<ClientModel> Login(AccountModel account)
        {
            var parameters = new 
            {
               AgencyNumber = account.Agency.Number,
               AgencyDigit = account.Agency.Digit,
               AccountNumber = account.Number,
               AccountDigit = account.Digit
            };

            var result = await this.LoadData<dynamic>(
                "dbo.spClient_Login", parameters, false);
            
            return result.FirstOrDefault();
        }

        public async Task<long> Create(ClientModel client)
        {
            var parameters = new
            {
                client.Name,
                client.Cpf
            };

            return await this.SaveDataAndReturn<long, dynamic>("dbo.spClient_Create", parameters);
        }

        public Task Delete(long id)
        {
            return this.SaveData("dbo.spClient_Delete", new { Id = id });
        }

        private async Task<IEnumerable<ClientModel>> LoadData<T>(string storedProcedure, T parameters, bool includeAgency = true)
        {
            if(includeAgency)
            {
                return await this.LoadDataWithAgency<T>(storedProcedure, parameters);
            }
            else
            {
                return await this.LoadDataWithoutAgency<T>(storedProcedure, parameters);
            }
        }

        private async Task<IEnumerable<ClientModel>> LoadDataWithAgency<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = _connection.GetConnection())
            {
                return await connection.QueryAsync<ClientModel, AccountModel, AgencyModel, ClientModel>(
                    storedProcedure,
                    (client, account, agency) => this.MapResults(client, account, agency),
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        private async Task<IEnumerable<ClientModel>> LoadDataWithoutAgency<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = _connection.GetConnection())
            {
                return await connection.QueryAsync<ClientModel, AccountModel, ClientModel>(
                    storedProcedure,
                    (client, account) => this.MapResults(client, account),
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        private ClientModel MapResults(
            ClientModel client, 
            AccountModel account, 
            AgencyModel agency = null
        )
        {
            account.Agency = agency;
            client.Account = account;
            return client;
        }
    }
}
