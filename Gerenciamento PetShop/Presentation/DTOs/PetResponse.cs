using Gerenciamento_PetShop.Domain.Enums;
using Gerenciamento_PetShop.Domain.Modelos;

namespace Gerenciamento_PetShop.Presentation.DTOs
{
    public class PetResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? TipoAnimal { get; set; }
        public string PorteAnimal { get; set; }
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
    }
}
