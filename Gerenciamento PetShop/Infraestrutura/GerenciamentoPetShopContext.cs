using Gerenciamento_PetShop.Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_PetShop.Infraestrutura
{
    public class GerenciamentoPetShopContext : DbContext
    {
        public DbSet<Clientes> Clientes { get; set; }

        public GerenciamentoPetShopContext(DbContextOptions<GerenciamentoPetShopContext> options)
        : base(options)
        {
        }
    }
}
