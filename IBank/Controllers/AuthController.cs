using IBank.Dtos.Auth;
using IBank.Exceptions;
using IBank.Services.Auth;
using IBank.Services.Client;
using IBank.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IBank.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IClientService _clientService;
        private readonly ITokenService _tokenService;

        public AuthController(
            IAuthService authService,
            IClientService clientService,
            ITokenService tokenService)
        {
            _authService = authService;
            _clientService = clientService;
            _tokenService = tokenService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<MeAuthDto>> Me()
        {
            long id = long.Parse(_tokenService.GetIdFromToken(User));

            try
            {
                var me = await _clientService.Get(id);
                return Ok(me);
            }
            catch (ClientNotFoundException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenAuthDto>> Login(LoginAuthDto login)
        {
            try
            {
                var token = await _authService.Login(login);
                return Ok(token);
            }
            catch(AccountNotFoundException e)
            {
                return Unauthorized(new { e.Message });
            }
        }
    }
}
