using Gerenciamento_PetShop.Domain.Modelos;

namespace Gerenciamento_PetShop.Application.Interfaces
{
    public interface IFileStorageService
    {
        string SalvarArquivo(IFormFile arquivo);
        byte[] LerArquivo(string caminho);
    }
}