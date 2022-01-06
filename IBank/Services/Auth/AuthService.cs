using AutoMapper;
using DataAccess.Data.Client;
using DataAccess.Models;
using IBank.Dtos.Auth;
using IBank.Exceptions;
using IBank.Services.Token;
using IBank.Settings;
using Isopoh.Cryptography.Argon2;
using System.Threading.Tasks;

namespace IBank.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IClientData _clientData;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IJwtSettings _jwt;
        public AuthService(
            IClientData clientData,
            ITokenService tokenService,
            IMapper mapper,
            IJwtSettings jwt
        )
        {
            _clientData = clientData;
            _tokenService = tokenService;
            _mapper = mapper;
            _jwt = jwt;
        }

        public async Task<TokenAuthDto> Login(LoginAuthDto login)
        {
            var client = await _clientData.Login(_mapper.Map<AccountModel>(login));

            if (client == null || !Argon2.Verify(client.Account.ShortPassword, login.ShortPassword))
            {
                throw new AccountNotFoundException("No account was found with the provided credentials");
            }

            var token = _tokenService.GenerateToken(client.Id.ToString(), client.Name);

            return new TokenAuthDto(int.Parse(_jwt.ExpirationInSeconds), token);
        }
    }

}
