using Gerenciamento_PetShop.Application.Services;
using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Infraestrutura;
using Gerenciamento_PetShop.Presentation.ViewModel;
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public void Baixar_DeveRetornarArquivoCorreto()
        {
            var repoMock = new Mock<IClientesRepository>();
            var fileMock = new Mock<IFileStorageService>();
            var service = new ClientesService(repoMock.Object, fileMock.Object);

            var pageNumber = 1;
            var pageQuantity = 5;
            var cpfDesejado = "12312452323";
            var clientesFake = new List<Clientes> {
                new Clientes { CPF = "12312312323", Nome = "Cliente 1" },
                new Clientes { CPF = cpfDesejado, Nome = "Cliente 2" }
            };
            repoMock
                .Setup(s => s.Get(pageNumber, pageQuantity))
                .Returns(clientesFake);
            // 2. ACT - Chamamos a ação real
            var arquivo = service.Baixar(cpfDesejado);
            fileMock.Verify(f => f.LerArquivo(It.Is<string>(c => c == arquivo.ToString())), Times.Once);
        }
    }
}