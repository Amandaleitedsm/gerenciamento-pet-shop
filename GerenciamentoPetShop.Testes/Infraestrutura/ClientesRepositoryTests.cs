using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoPetShop.Testes.Infraestrutura
{
    public class ClientesRepositoryTests
    {
        [Fact]
        public void Repository_DeveSalvarClienteNoBancoReal()
        {
            // 1. ARRANGE - Configuramos um banco de dados "de mentira" que vive na RAM
            var options = new DbContextOptionsBuilder<GerenciamentoPetShopContext>()
                .UseInMemoryDatabase(databaseName: "PetShopTeste")
                .Options;

            using (var context = new GerenciamentoPetShopContext(options))
            {
                var repository = new ClientesRepository(context);
                var novoCliente = new Clientes("123", "Rex", DateTime.Now);

                // 2. ACT - Chamamos a ação real
                repository.Add(novoCliente);
                context.SaveChanges();
            }

            // 3. ASSERT - Abrimos uma nova conexão para ver se o dado persistiu
            using (var context = new GerenciamentoPetShopContext(options))
            {
                var clienteNoBanco = context.Clientes.FirstOrDefault(c => c.CPF == "123");
                Assert.NotNull(clienteNoBanco);
                Assert.Equal("Rex", clienteNoBanco.Nome);
            }
        }

       
    }
}
