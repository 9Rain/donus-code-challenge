using IBank.Dtos.Account;
using IBank.Exceptions;
using IBank.Services.Account;
using IBank.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IBank.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(
            IAccountService accountService,
            ITokenService tokenService
        )
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("my")]
        public async Task<ActionResult<ReturnAccountDto>> Get()
        {
            long id = long.Parse(_tokenService.GetIdFromToken(User));

            try
            {
                var account = await _accountService.GetByClientId(id);
                return Ok(account);
            }
            catch (AccountNotFoundException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [HttpGet]
        [Route("balance")]
        public async Task<ActionResult<ReturnAccountBalanceDto>> GetBalance()
        {
            long id = long.Parse(_tokenService.GetIdFromToken(User));

            try
            {
                var balance = await _accountService.GetBalance(id);
                return Ok(balance);
            }
            catch(AccountNotFoundException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CreateAccountDto>> Create(CreateAccountDto account)
        {
            try
            {
                var returnAccount = await _accountService.Create(account);
                return CreatedAtAction("Get", returnAccount);
            }
            catch(ClientAlreadyHasAnAccountException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { e.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            long id = long.Parse(_tokenService.GetIdFromToken(User));

            await _accountService.DeleteByClientId(id);

            return NoContent();
        }
    }
}
