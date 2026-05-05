using System.Net;

namespace Gerenciamento_PetShop.Presentation.DTOs
{
    public class ClientesResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
