using Gerenciamento_PetShop.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Gerenciamento_PetShop.Infrastructure.Storage 
{
    public class FileStorageService : IFileStorageService
    {
        public string SalvarArquivo(IFormFile arquivo)
        {
            if (!Directory.Exists("Storage"))
                Directory.CreateDirectory("Storage");

            var filePath = Path.Combine("Storage", arquivo.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                arquivo.CopyTo(stream);
            }

            return filePath;
        }

        public byte[] LerArquivo(string caminho)
        {
            if (string.IsNullOrEmpty(caminho) || !File.Exists(caminho))
                return Array.Empty<byte>();

            return File.ReadAllBytes(caminho);
        }
    }
}