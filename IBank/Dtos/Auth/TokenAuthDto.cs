using Microsoft.Extensions.Configuration;

namespace IBank.Dtos.Auth
{
    public class TokenAuthDto
    {
        public TokenAuthDto(IConfiguration config, string accessToken)
        {
            AccessToken = accessToken;
            ExpiresIn = int.Parse(config.GetSection("AppSettings:Jwt:ExpirationInSeconds").Value);
        }

        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
    }
}
