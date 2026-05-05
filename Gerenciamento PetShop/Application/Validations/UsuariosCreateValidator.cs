using FluentValidation;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Validations
{
    public class UsuariosCreateValidator : AbstractValidator<UsuariosCreateViewModel>
    {
        public UsuariosCreateValidator() { 
            RuleFor(u => u.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Length(11).WithMessage("O CPF deve conter exatamente 11 dígitos.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve conter pelo menos 6 caracteres.");
        }
    }
}
