using Asp.Versioning;
using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Application.Services;
using Gerenciamento_PetShop.Presentation.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_PetShop.Presentation.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[Controller]")]
    [ApiVersion("1.0")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService _clientesService;

        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] ClientesCreateViewModel clientesViewModel)
        {
            _clientesService.AdicionarCliente(clientesViewModel);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/download")]
        public IActionResult Download(int id)
        {
            var dataBytes = _clientesService.Baixar(id);
            if (dataBytes == null)
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

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clientesService.GetClienteById(id);
            if (cliente == null) return NotFound("Cliente não encontrado.");
            return Ok(cliente);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] ClientesUpdateViewModel clientesUpdateViewModel)
        {
            var clienteAtualizado = _clientesService.AtualizarCliente(id, clientesUpdateViewModel);
            if (clienteAtualizado == null) return NotFound("Cliente não encontrado para atualização.");
            return Ok(clienteAtualizado);
        }

        [HttpGet]
        [Route("relatorio")]
        public IActionResult GetRelatorioClientes(int pageNumber, int pageQuantity)
        {
            var relatorio = _clientesService.GetRelatorioClientes(pageNumber, pageQuantity);
            return Ok(relatorio);
        }

        [HttpGet]
        [Route("relatorio/tipo-animal")]
        public IActionResult GetRelatorioPetsPorTipo(int pageNumber, int pageQuantity, int tipoAnimal)
        {
            throw new Exception("Teste de log da Amanda: Erro proposital!");
            var relatorio = _clientesService.GetRelatorioPetsPorTipo(pageNumber, pageQuantity, tipoAnimal);
            return Ok(relatorio);
        }
    }
}
