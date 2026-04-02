using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciamento_PetShop.Domain.Modelos
{
    [Table ("clientes")]
    public class Clientes
    {
        [Key]
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string? Photo { get; set; }
        public DateTime? Data_Nascimento { get; set; }
        public List<Pets> Pets { get; set; } = new List<Pets> ();

        public Clientes(string cpf, string nome, DateTime? data_nascimento = null, string? photo = null)
        {
            CPF = cpf;
            Nome = nome;
            Data_Nascimento = data_nascimento;
            Photo = photo;
        }

        public Clientes() { }

    }
}
