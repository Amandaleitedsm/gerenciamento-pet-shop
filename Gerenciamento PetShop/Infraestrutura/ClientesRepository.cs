using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
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
                return _context.Clientes.ToList();
            }
            return _context.Clientes.Skip((pageNumber - 1) * pageQuantity).Take(pageQuantity).ToList();
        }

        public Clientes? Get(string cpf)
        {
            return _context.Clientes.Find(cpf);
        }
    }
}
