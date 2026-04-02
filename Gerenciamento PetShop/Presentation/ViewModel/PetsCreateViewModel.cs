using Gerenciamento_PetShop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_PetShop.Presentation.ViewModel
{
    public class PetsCreateViewModel
    {
        public string? Nome { get; set; }
        public TipoAnimalEnum TipoAnimal { get; set; }
        public PorteAnimalEnum PorteAnimal { get; set; }
        public int ClienteId { get; set; }
    }
}
