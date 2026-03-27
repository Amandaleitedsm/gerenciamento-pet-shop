using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_PetShop.Presentation.ViewModel
{
    public class ClientesViewModel
    {
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }
        public IFormFile? Photo { get; set; }

        /// <summary>
        /// Data de nascimento do cliente.
        /// </summary>
        public DateTime? DataNascimento { get; set; }
    }
}
