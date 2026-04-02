using Gerenciamento_PetShop.Domain.Modelos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_PetShop.Presentation.ViewModel
{
    public class ClientesCreateViewModel
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public IFormFile? Photo { get; set; }

        /// <summary>
        /// Data de nascimento do cliente.
        /// </summary>
        public DateTime? DataNascimento { get; set; }
    }
}
