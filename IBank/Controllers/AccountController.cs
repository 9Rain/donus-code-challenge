using IBank.Dtos.Account;
using Microsoft.AspNetCore.Mvc;

namespace IBank.Controllers
{
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountController : ControllerBase
    {
        /*private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }*/

        [HttpGet]
        [Route("my")]
        public IActionResult Get()
        {
            // Obter id do usuario logado
            // Chamar service com id

            return Ok(new ReturnAccountDto());
        }

        [HttpPost]
        public IActionResult Create(CreateAccountDto account)
        {
            // Chamar service
            // Retornar erro caso cpf já exista

            return CreatedAtAction("Get", new ReturnAccountDto());
        }

        [HttpDelete]
        public IActionResult Delete(DeleteAccountDto account)
        {
            // Chamar service
            return NoContent();
        }
    }
}
