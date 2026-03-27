using Asp.Versioning;
using Gerenciamento_PetShop.Application.Services;
using Gerenciamento_PetShop.Domain.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_PetShop.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "amanda" && password == "a1234")
            {
                var token = TokenService.GenerateToken(new Clientes(cpf: "55566677788", nome: username));
                return Ok(token);
            }
            return BadRequest("username or password invalid");
        }
    }
}
