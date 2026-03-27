using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Domain.Interfaces
{
    public interface IClientesService
    {
        void AdicionarCliente(ClientesViewModel clientesViewModel);
        byte[] Baixar(string cpf);
        List<Clientes> GetClientes(int pageNumber, int pageQuantity);
    }
}
