using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;

namespace Gerenciamento_PetShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuariosServices _usuariosServices;

        public AuthService(IUsuariosServices usuariosServices)
        {
            _usuariosServices = usuariosServices;
        }

        public AuthResponse ValidarUsuario(Usuarios? usuario, string cpf, string senha)
        {
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                throw new Exception("CPF ou senha inválidos.");
            }
            return TokenService.GenerateToken(new Usuarios(usuario.Cpf, usuario.Senha));
        }
    }
}
