using Gerenciamento_PetShop.Domain.Enums;

namespace Gerenciamento_PetShop.Presentation.DTOs
{
    public class PetUpdateResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? TipoAnimal { get; set; }
        public string PorteAnimal { get; set; }
        public int ClienteId { get; set; }
    }
}
