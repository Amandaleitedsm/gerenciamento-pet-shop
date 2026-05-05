using Gerenciamento_PetShop.Application.Services;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;

namespace Gerenciamento_PetShop.Application.Interfaces
{
    public interface IAuthService
    {
        AuthResponse ValidarUsuario(Usuarios? usuario, string cpf, string senha);
    }
}
