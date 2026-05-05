using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;

namespace Gerenciamento_PetShop.Infraestrutura
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly GerenciamentoPetShopContext _context;
        public UsuariosRepository(GerenciamentoPetShopContext context)
        {
            _context = context;
        }
        public void Add(Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuarios? Get(int id)
        {
           return _context.Usuarios
                .FirstOrDefault(u => u.Id == id);
        }

        public List<Usuarios> Get(int pageNumber, int pageQuantity)
        {
            if (pageNumber < 1 || pageQuantity < 1)
            {
                return _context.Usuarios.ToList();
            }
            return _context.Usuarios
                .Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .ToList();
        }

        public Usuarios? GetByCpf(string cpf)
        {
            return _context.Usuarios
                .FirstOrDefault(u => u.Cpf == cpf);
        }

        public void Update(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
        }
    }
}
