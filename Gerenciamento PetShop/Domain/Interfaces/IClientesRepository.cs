using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Domain.Interfaces
{
    public interface IClientesRepository
    {
        void Add(Clientes cliente);
        List<Clientes> Get(int pageNumber, int pageQuantity);
        Clientes? Get(int id);
        void Update(Clientes cliente);
        List<RelatorioClientesResponse> GetRelatorioClientes(int pageNumber, int pageQuantity);
        List<RelatorioClientesResponse> GetRelatorioPorCliente(int id);
        List<RelatorioPetsPorTipo> GetRelatorioPetsPorTipo(int pageNumber, int pageQuantity, int tipoAnimal);
    }
}
