using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Connection
{
    public class SqlConnection : IConnection
    {
        private readonly IConfiguration _config;

        public SqlConnection(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection GetConnection(string connectionId = "Default")
        {
            return new System.Data.SqlClient.SqlConnection(_config.GetConnectionString(connectionId));
        }
    }
}
