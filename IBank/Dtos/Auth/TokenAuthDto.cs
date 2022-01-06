using Microsoft.Extensions.Configuration;

namespace IBank.Dtos.Auth
{
    public class TokenAuthDto
    {
        public TokenAuthDto(int expiresIn, string accessToken)
        {
            ExpiresIn = expiresIn;
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
    }
}
