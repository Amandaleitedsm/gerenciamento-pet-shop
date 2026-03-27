using Asp.Versioning;
using Gerenciamento_PetShop.Presentation.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gerenciamento_PetShop.Domain.Interfaces;

namespace Gerenciamento_PetShop.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/clientes")]
    [ApiVersion("1.0")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService _clientesService;
        
        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        [HttpPost]
        public IActionResult Add([FromForm] ClientesViewModel clientesViewModel)
        {
            _clientesService.AdicionarCliente(clientesViewModel);
            return Ok();
        }

        [HttpPost]
        [Route("{cpf}/download")]
        public IActionResult Download(string cpf)
        {
            var dataBytes = _clientesService.Baixar(cpf);
            if (dataBytes == null || dataBytes.Length == 0)
            {
                return NotFound("Foto não encontrada.");
            }
            return File(dataBytes, "image/png");
        }
        
        /// <summary>
        /// Busca todos os clientes
        /// </summary>
        /// <description>Busca todos os clientes, podendo filtrar por página (equivalência 1 -> página 1) com quantidades de clientes</description>
        /// <param name="pageNumber">Ordem da página que deseja buscar</param>
        /// <param name="pageQuantity">Quantos clientes devem existir por página</param>
        /// <returns>Dados dos clientes cadastrados</returns>
        /// <response code="200">Clientes retornados</response>
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var clientes = _clientesService.GetClientes(pageNumber, pageQuantity);
            return Ok(clientes);
        }
    }
}
