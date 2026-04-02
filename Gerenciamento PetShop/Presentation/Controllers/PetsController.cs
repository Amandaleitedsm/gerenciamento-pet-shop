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
    public class PetsController : Controller
    {
        public readonly IPetsService _petsService;

        public PetsController(IPetsService petsService)
        {
            _petsService = petsService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] PetsCreateViewModel petsViewModel)
        {
            _petsService.AdicionarPet(petsViewModel);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var pets = _petsService.GetPets(pageNumber, pageQuantity);
            return Ok(pets);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var pet = _petsService.GetPetById(id);
            if (pet == null) return NotFound("Pet não encontrado.");
            return Ok(pet);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] PetsUpdateViewModel petsUpdateViewModel)
        {
            var petAtualizado = _petsService.AtualizarPet(id, petsUpdateViewModel);
            if (petAtualizado == null) return NotFound("Pet não encontrado para atualização.");
            return Ok(petAtualizado);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_petsService.DeletarPet(id)) return NotFound("Pet não encontrado para exclusão.");
            return Ok();
        }
    }
}
