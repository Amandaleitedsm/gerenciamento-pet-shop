using Gerenciamento_PetShop.Domain.Enums;
using System.Text.Json.Serialization;

namespace Gerenciamento_PetShop.Domain.Modelos
{
    public class Pets
    {
        public Pets(string nome, TipoAnimalEnum tipoAnimal, PorteAnimalEnum porteAnimal, int clienteId)
        {
            Nome = nome;
            TipoAnimal = tipoAnimal;
            PorteAnimal = porteAnimal;
            ClienteId = clienteId;
        }

        public Pets()
        {
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoAnimalEnum? TipoAnimal { get; set; }
        public PorteAnimalEnum PorteAnimal { get; set; }
        public int ClienteId { get; set; }     // FK

        [JsonIgnore]
        public Clientes Cliente { get; set; }  // navegação
    }
}
