using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Interfaces
{
    public interface IClientesService
    {
        void AdicionarCliente(ClientesCreateViewModel clientesViewModel);
        byte[] Baixar(int id);
        List<ClientesResponse> GetClientes(int pageNumber, int pageQuantity);
        ClientesResponse GetClienteById(int id);
        ClientesResponse AtualizarCliente(int id, ClientesUpdateViewModel clientesViewModel);
    }
}
