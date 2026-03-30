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


            var caminhoEsperado = "C:\\Users\\amanda.machado\\OneDrive - GSW Software\\Documentos\\Estudos - Estagio GSW\\FASE 5\\APIS REST\\Gerenciamento PetShop\\Gerenciamento PetShop\\Storage\\images.png";
            var cpfDesejado = "12312452323";
            var bytesFalsos = new byte[] { 0x20, 0x20, 0x20 };

            var clienteFake = new Clientes { CPF = cpfDesejado, Nome = "Cliente 1", Photo = caminhoEsperado};
            repoMock
                 .Setup(s => s.Get(cpfDesejado))
                 .Returns(clienteFake);

            // Se o seu método LerArquivo retorna bytes ou string, você pode configurar o retorno aqui:
            fileMock
                .Setup(f => f.LerArquivo(caminhoEsperado))
                .Returns(bytesFalsos);

            // 2. ACT - Chamamos a ação real
            var resultado = service.Baixar(cpfDesejado);
            fileMock.Verify(f => f.LerArquivo(It.Is<string>(path => path == caminhoEsperado)), Times.Once);

            // Verifica se o service realmente retornou os bytes que o mock enviou
            Assert.Equal(bytesFalsos, resultado);
        }

        [Fact]
        public void GetClientes_DeveRetornarListaDeClientes()
        {
            var repoMock = new Mock<IClientesRepository>();
            var fileMock = new Mock<IFileStorageService>();
            var service = new ClientesService(repoMock.Object, fileMock.Object);

            var clientesFake = new List<Clientes>
            {
                new Clientes { CPF = "12345678", Nome = "Cliente 1" },
                new Clientes { CPF = "87654321", Nome = "Cliente 2" }
            };

            var pageNumber = 1;
            var pageQuantity = 10;
            repoMock
                 .Setup(s => s.Get(pageNumber, pageQuantity))
                 .Returns(clientesFake);

            var resultado = service.GetClientes(pageNumber, pageQuantity);

            repoMock.Verify(r => r.Get(It.Is<int>(p => p == pageNumber), It.Is<int>(q => q == pageQuantity)), Times.Once);

            Assert.Equal(clientesFake, resultado);
        }
    }
}