using IBank.Dtos.Auth;
using System.Threading.Tasks;

namespace IBank.Services.Auth
{
    public interface IAuthService
    {
        Task<TokenAuthDto> Login(LoginAuthDto login);
    }
}
