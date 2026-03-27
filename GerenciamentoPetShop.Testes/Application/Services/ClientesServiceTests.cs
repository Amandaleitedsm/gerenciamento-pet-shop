using Gerenciamento_PetShop.Application.Services;
using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.ViewModel;
using Moq;  
using Xunit;

namespace Gerenciamento_PetShop.Testes.Application.Services
{
    public class ClientesServiceTests
    {
        [Fact]
        public void AdicionarCliente_DeveChamarRepositorio_QuandoDadosForemValidos()
        {
            // 1. ARRANGE
            var repoMock = new Mock<IClientesRepository>();
            // Se o seu service também pedir o IFileStorageService no construtor, mock ele também:
            var fileMock = new Mock<IFileStorageService>();

            // Passamos os Mocks para o construtor
            var service = new ClientesService(repoMock.Object, fileMock.Object);

            var viewModel = new ClientesViewModel
            {
                Nome = "João Silva",
                Cpf = "12345678",
                DataNascimento = new DateTime(1990, 5, 20),
                Photo = null // Testando primeiro o cenário sem foto
            };

            // 2. ACT
            service.AdicionarCliente(viewModel);

            // 3. ASSERT (O segredo está aqui!)
            // Verificamos se o método Add do repositório foi chamado exatamente UMA VEZ
            // com um objeto do tipo Clientes que tenha o nome "João Silva"
            repoMock.Verify(r => r.Add(It.Is<Clientes>(c => c.Nome == "João Silva")), Times.Once);
        }
    }
}