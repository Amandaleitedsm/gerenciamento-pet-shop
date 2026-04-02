using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Domain.Interfaces
{
    public interface IClientesRepository
    {
        void Add(Clientes cliente);
        List<Clientes> Get(int pageNumber, int pageQuantity);
        Clientes? Get(int id);
        void Update(Clientes cliente);
    }
}
