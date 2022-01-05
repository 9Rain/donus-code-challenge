using Dapper;
using DataAccess.Connection;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public abstract class DataManager
    {
        private readonly IConnection _connection;

        protected DataManager(IConnection connection)
        {
            _connection = connection;
        }

        protected async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using (IDbConnection connection = _connection.GetConnection())
            {
                return await connection.QueryAsync<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        protected async Task SaveData<T>(string storedProcedure, T parameters)
        {
            using (IDbConnection connection = _connection.GetConnection())
            {
                await connection.ExecuteAsync(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        protected async Task<T> SaveDataAndReturn<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
        {
            using (IDbConnection connection = _connection.GetConnection())
            {
                return await connection.QuerySingleAsync<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
            }
        }
    }
}
