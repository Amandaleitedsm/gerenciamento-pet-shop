using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_PetShop.Infraestrutura
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly GerenciamentoPetShopContext _context;

        public ClientesRepository(GerenciamentoPetShopContext context)
        {
            _context = context;
        }
        public void Add(Clientes cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }
        

        public List<Clientes> Get(int pageNumber, int pageQuantity)
        {
            if (pageNumber < 1 || pageQuantity < 1)
            {
                return _context.Clientes
                    .ToList();
            }
            return _context.Clientes
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public Clientes? Get(int id)
        {
            return _context.Clientes
                .Include(c => c.Pets)
                .FirstOrDefault(c => c.Id == id);
        }

        public List<RelatorioClientesResponse> GetRelatorioPorCliente(int id)
        {
            var parametroId = new SqlParameter("@id", id);
            return _context.Database
                .SqlQueryRaw<RelatorioClientesResponse>("EXEC RelatorioPorCliente @id", parametroId)
                .ToList();
        }

        public List<RelatorioClientesResponse> GetRelatorioClientes(int pageNumber, int pageQuantity)
        {
            if (pageNumber < 1 || pageQuantity < 1)
            {
                return _context.Database
                    .SqlQueryRaw<RelatorioClientesResponse>("EXEC RelatorioClientes")
                    .ToList();
            }
            return _context.Database
                .SqlQueryRaw<RelatorioClientesResponse>("EXEC RelatorioClientes")
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public List<RelatorioPetsPorTipo> GetRelatorioPetsPorTipo(int pageNumber, int pageQuantity, int tipoAnimal)
        {
            var parametroTipo = new SqlParameter("@TipoAnimal", tipoAnimal);
            if (pageNumber < 1 || pageQuantity < 1)
            {
                return _context.Database
                    .SqlQueryRaw<RelatorioPetsPorTipo>("EXEC RelatorioPetsPorTipo @TipoAnimal", parametroTipo)
                    .ToList();
            }
            return _context.Database
                .SqlQueryRaw<RelatorioPetsPorTipo>("EXEC RelatorioPetsPorTipo")
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public void Update(Clientes cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }
    }
}
