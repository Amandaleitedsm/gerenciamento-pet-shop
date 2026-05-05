using Asp.Versioning;
using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Presentation.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_PetShop.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServices _usuariosServices;

        public UsuariosController(IUsuariosServices usuariosServices)
        {
            _usuariosServices = usuariosServices;
        }

        [HttpPost]
        public IActionResult Add([FromBody] UsuariosCreateViewModel usuario)
        {
            _usuariosServices.AdicionarUsuario(usuario);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var usuarios = _usuariosServices.ObterUsuarios(pageNumber, pageQuantity);
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _usuariosServices.ObterUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        [Route("cpf/{cpf}")]
        public IActionResult GetByCpf(string cpf)
        {
            var usuario = _usuariosServices.ObterUsuarioPorCpf(cpf);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] UsuariosUpdateViewModel usuarioUpdateViewModel)
        {
            _usuariosServices.AtualizarUsuario(id, usuarioUpdateViewModel);
            return Ok();
        }
    }
}
