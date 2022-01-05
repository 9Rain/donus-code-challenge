using AutoMapper;
using DataAccess.Data.Client;
using DataAccess.Models;
using IBank.Dtos.Auth;
using IBank.Exceptions;
using IBank.Services.Token;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace IBank.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IClientData _clientData;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthService(
            IClientData clientData, 
            ITokenService tokenService, 
            IMapper mapper,
            IConfiguration config
        )
        {
            _clientData = clientData;
            _tokenService = tokenService;
            _mapper = mapper;
            _config = config;
        }

        public async Task<TokenAuthDto> Login(LoginAuthDto login)
        {   
            var client = await _clientData.Login(_mapper.Map<AccountModel>(login));

            if (client == null || !Argon2.Verify(client.Account.ShortPassword, login.ShortPassword))
            {
                throw new AccountNotFoundException("No account was found with the provided credentials");
            }

            var token = _tokenService.GenerateToken(client.Id.ToString(), client.Name);

            return new TokenAuthDto(_config, token);
        }
    }

}
