using FluentValidation;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Validations
{
    public class ClientesUpdateValidator : AbstractValidator<ClientesUpdateViewModel>
    {
        public ClientesUpdateValidator()
        {
            RuleFor(c => c.Nome)
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Nome));

            RuleFor(c => c.DataNascimento)
                .LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser no passado.")
                .When(c => c.DataNascimento.HasValue);

            RuleFor(p => p.Photo)
                // CascadeMode.Stop garante que se a foto for nula, ele para de validar as regras seguintes
                // para não dar erro de NullReferenceException ao verificar o tamanho
                .Cascade(CascadeMode.Stop)

                .NotNull().WithMessage("É obrigatório enviar uma foto.")

                // Valida se o ficheiro não tem 0 bytes
                .Must(f => f.Length > 0).WithMessage("O ficheiro enviado está vazio.")

                // Valida o limite de tamanho (ex: 2 MB = 2 * 1024 * 1024 bytes)
                .Must(f => f.Length <= 2097152).WithMessage("A foto não pode ultrapassar 2MB.")

                // Valida a extensão / ContentType (segurança contra ficheiros maliciosos)
                .Must(ValidarTipoFicheiro).WithMessage("Apenas imagens nos formatos JPG, JPEG ou PNG são permitidas.");
        }

        // Método auxiliar para manter o código limpo
        private bool ValidarTipoFicheiro(IFormFile foto)
        {
            var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/jpg" };
            return tiposPermitidos.Contains(foto.ContentType);
        }
    }
}