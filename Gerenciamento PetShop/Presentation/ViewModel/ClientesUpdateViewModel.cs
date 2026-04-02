using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_PetShop.Presentation.ViewModel
{
    public class ClientesUpdateViewModel
    {
        public string? Nome { get; set; }
        public IFormFile? Photo { get; set; }
        /// <summary>
        /// Data de nascimento do cliente.
        /// </summary>
        public DateTime? DataNascimento { get; set; }
    }
}
