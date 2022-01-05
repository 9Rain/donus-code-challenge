using System.Security.Claims;

namespace IBank.Services.Token
{
    public interface ITokenService
    {
        string GetIdFromToken(ClaimsPrincipal User);
        string GenerateToken(string id, string name);
    }
}
