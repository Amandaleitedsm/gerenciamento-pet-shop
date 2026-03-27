using Gerenciamento_PetShop.Domain.Interfaces;
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
            var cpf = "12345665434";
            var bytesFakes = new byte[] { 0x01, 0x02 };
            _serviceMock
                .Setup(s => s.Baixar(cpf))
                .Returns(bytesFakes);

            var resultado = _controller.Download(cpf);

            Assert.IsType<FileContentResult>(resultado);
        }
    }
}
