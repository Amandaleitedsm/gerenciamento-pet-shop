using Gerenciamento_PetShop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_PetShop.Presentation.ViewModel
{
    public class PetsCreateViewModel
    {
        [Required(ErrorMessage = "O nome do animal deve ser preenchido")]
        public string? Nome { get; set; }
        public TipoAnimalEnum TipoAnimal { get; set; }
        public PorteAnimalEnum PorteAnimal { get; set; }

        [Required(ErrorMessage = "O id do cliente responsável pelo animal deve ser preenchido")]
        public int ClienteId { get; set; }
    }
}
