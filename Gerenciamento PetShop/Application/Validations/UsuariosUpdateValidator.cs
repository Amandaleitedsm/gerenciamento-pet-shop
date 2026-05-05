using FluentValidation;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Validations
{
    public class UsuariosUpdateValidator : AbstractValidator<UsuariosUpdateViewModel>
    {
        public UsuariosUpdateValidator() {
            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve conter pelo menos 6 caracteres.");
        }
    }
}
