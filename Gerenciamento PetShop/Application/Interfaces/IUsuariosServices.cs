using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Interfaces
{
    public interface IUsuariosServices
    {
        void AdicionarUsuario(UsuariosCreateViewModel usuario);
        Usuarios? ObterUsuario(int id);
        List<Usuarios> ObterUsuarios(int pageNumber, int pageQuantity);
        void AtualizarUsuario(int id, UsuariosUpdateViewModel usuario);
        Usuarios? ObterUsuarioPorCpf(string cpf);
    }
}
