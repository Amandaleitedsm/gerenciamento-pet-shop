using FluentValidation;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Validations
{
    public class PetsUpdateValidator : AbstractValidator<PetsUpdateViewModel>
    {
        public PetsUpdateValidator() 
        {
            RuleFor(c => c.Nome)
                .Length(3, 100).WithMessage("O novo nome deve ter entre 3 e 100 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Nome));

            RuleFor(c => c.TipoAnimal)
                .IsInEnum().WithMessage("O tipo do animal é inválido.")
                .When(c => c.TipoAnimal.HasValue);

            RuleFor(c => c.PorteAnimal)
                .IsInEnum().WithMessage("O porte do animal é inválido.")
                .When(c => c.PorteAnimal.HasValue);

            RuleFor(c => c.ClienteId)
                .GreaterThan(0).WithMessage("O ID do cliente é inválido.")
                .When(c => c.ClienteId.HasValue);
        }
    }
}
