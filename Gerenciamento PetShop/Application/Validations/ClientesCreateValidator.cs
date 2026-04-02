using FluentValidation;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Validations
{
    public class ClientesCreateValidator : AbstractValidator<ClientesCreateViewModel>
    {
        public ClientesCreateValidator()
        {
            // Validação do Nome
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

            // Validação do CPF (Básica)
            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Length(11).WithMessage("O CPF deve conter exatamente 11 dígitos.");

            RuleFor(c => c.DataNascimento)
                .LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser no passado.");

            RuleFor(c => c.Photo)
                .Must(file => file == null || file.ContentType.StartsWith("image/"))
                .WithMessage("A foto deve ser um arquivo de imagem.");
        }
    }
}