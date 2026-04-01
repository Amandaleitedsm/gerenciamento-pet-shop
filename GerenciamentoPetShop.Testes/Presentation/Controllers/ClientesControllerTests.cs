using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoPetShop.Testes.Presentation.Controllers
{
    public class ClientesControllerTests
{
        private readonly Mock<IClientesService> _serviceMock;
        private readonly ClientesController _controller;

        public ClientesControllerTests()
        {
            _serviceMock = new Mock<IClientesService>();
            _controller = new ClientesController(_serviceMock.Object);
        }

        [Fact]
        public void Download_DeveRetornarFile_QuandoServiceRetornaBytes()
        {
            var id = 1;
            var bytesFakes = new byte[] { 0x01, 0x02 };
            _serviceMock
                .Setup(s => s.Baixar(id))
                .Returns(bytesFakes);

            var resultado = _controller.Download(id);

            Assert.IsType<FileContentResult>(resultado);
        }

        public void Get_DeveRetornarOkComClientes_QuandoServiceRetornaClientes()
        {
            var pageNumber = 1;
            var pageQuantity = 10;
            var clientesFakes = new List<Clientes>
            {
                new Clientes { CPF = "12312312323", Nome = "Cliente 1"},
                new Clientes { CPF = "12312452323", Nome = "Cliente 2" }
            };
            _serviceMock
                .Setup(s => s.GetClientes(pageNumber, pageQuantity)) // Configura o mock para retornar a lista de clientes fake quando o método GetClientes for chamado com os parâmetros pageNumber e pageQuantity
                .Returns(clientesFakes);
            var resultado = _controller.Get(pageNumber, pageQuantity) as OkObjectResult; // Chama o método Get e converte o resultado para OkObjectResult
            Assert.NotNull(resultado); // Verifica se o resultado não é nulo
            Assert.IsType<List<Clientes>>(resultado.Value); // Verifica se o valor retornado é do tipo List<Clientes>
        }
    }
}
