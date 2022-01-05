using System.Data;

namespace DataAccess.Connection
{
    public interface IConnection
    {
        IDbConnection GetConnection(string connectionId = "Default");
    }
}
