using Gerenciamento_PetShop.Domain.Modelos;

namespace Gerenciamento_PetShop.Domain.Interfaces
{
    public interface IClientesRepository
    {
        void Add(Clientes cliente);
        List<Clientes> Get(int pageNumber, int pageQuantity);

        Clientes? Get(string cpf);
    }
}
