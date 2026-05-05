using Gerenciamento_PetShop.Domain.Modelos;

namespace Gerenciamento_PetShop.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        void Add(Usuarios usuario);
        Usuarios? Get(int id);
        List<Usuarios> Get(int pageNumber, int pageQuantity);
        void Update(Usuarios usuario);
        Usuarios? GetByCpf(string cpf);
    }
}
