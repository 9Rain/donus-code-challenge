using DataAccess.Models;
using System.Threading.Tasks;

namespace DataAccess.Data.Client
{
    public interface IClientData
    {
        Task<ClientModel> Get(long id);
        Task<bool> HasActiveOrDisabledAccount(string cpf);
        Task<ClientModel> Login(AccountModel account);
        Task<long> Create(ClientModel client);
        Task Delete(long id);
    }
}
