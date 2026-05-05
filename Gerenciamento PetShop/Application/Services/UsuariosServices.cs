using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Services
{
    public class UsuariosServices : IUsuariosServices
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosServices(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public void AdicionarUsuario(UsuariosCreateViewModel usuario)
        {
            if (usuario == null) {
                throw new ArgumentNullException(nameof(usuario));
            }
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            var user = new Usuarios(usuario.Cpf, senhaHash);
            _usuariosRepository.Add(user);
        }

        public void AtualizarUsuario(int id, UsuariosUpdateViewModel usuarioUpdateViewModel)
        {
            var usuario = _usuariosRepository.Get(id);
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            usuario.Senha = usuarioUpdateViewModel.Senha;
            _usuariosRepository.Update(usuario);
        }

        public Usuarios? ObterUsuario(int id)
        {
            return _usuariosRepository.Get(id);
        }

        public Usuarios? ObterUsuarioPorCpf(string cpf)
        {
            return _usuariosRepository.GetByCpf(cpf);
        }

        public List<Usuarios> ObterUsuarios(int pageNumber, int pageQuantity)
        {
            return _usuariosRepository.Get(pageNumber, pageQuantity);
        }
    }
}
