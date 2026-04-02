using FluentValidation;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Validations
{
    public class PetsCreateValidator : AbstractValidator<PetsCreateViewModel>
    {
        public PetsCreateValidator() { 
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome do animal é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            RuleFor(p => p.TipoAnimal)
                .IsInEnum().WithMessage("O tipo do animal é inválido.");

            RuleFor(p => p.PorteAnimal)
                .IsInEnum().WithMessage("O porte do animal é inválido.");

            RuleFor(p => p.ClienteId)
                .GreaterThan(0)
                .WithMessage("O id do cliente responsável pelo animal deve ser preenchido.");
        }
    }
}
