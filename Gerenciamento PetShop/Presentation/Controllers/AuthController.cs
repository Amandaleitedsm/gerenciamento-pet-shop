using Asp.Versioning;
using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Application.Services;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_PetShop.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/v{version:apiVersion}/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUsuariosServices _usuariosServices;

        public AuthController(IAuthService authService, IUsuariosServices usuariosServices)
        {
            _authService = authService;
            _usuariosServices = usuariosServices;
        }

        [HttpPost]
        public IActionResult Auth(AuthViewModel authViewModel)
        {
            var usuario = _usuariosServices.ObterUsuarioPorCpf(authViewModel.Cpf);
            var token = _authService.ValidarUsuario(usuario, authViewModel.Cpf, authViewModel.Senha);
            return Ok(token);
        }
    }
}
