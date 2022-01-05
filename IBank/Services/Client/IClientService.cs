using DataAccess.Models;
using IBank.Dtos.Account;
using IBank.Dtos.Auth;
using System.Threading.Tasks;

namespace IBank.Services.Client
{
    public interface IClientService
    {
        Task<MeAuthDto> Get(long id);
        Task<ClientModel> Create(CreateAccountDto clientData);
    }
}
