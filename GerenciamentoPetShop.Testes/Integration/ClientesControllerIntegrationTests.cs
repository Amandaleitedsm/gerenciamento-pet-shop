using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Gerenciamento_PetShop.Testes.Integration
{
    // O 'IClassFixture' serve para subir a Program.cs da sua API
    public class ClientesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ClientesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // O Factory cria um "cliente de navegador" falso para bater na sua API
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetClientes_SemToken_DeveRetornar401()
        {
            // ACT - Tenta acessar a rota de clientes sem configurar o Bearer
            var response = await _client.GetAsync("/api/v1/Clientes");
            // a rota está com [Authorize] comentado
            // ASSERT - O status deve ser 401 (Unauthorized)
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}