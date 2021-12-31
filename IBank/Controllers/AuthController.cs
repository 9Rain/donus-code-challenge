using IBank.Dtos.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IBank.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginAuthDto login)
        {
            // Chamar service para logar
            return Ok(new TokenAuthDto());
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            // Chamar service para deslogar
            return NoContent();
        }

        [HttpGet]
        [Route("me")]
        public IActionResult Me()
        {
            // Chamar service para retornar dados do usuário logado
            return Ok(new MeAuthDto());
        }
    }
}
