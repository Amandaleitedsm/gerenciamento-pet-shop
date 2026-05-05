using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_PetShop.Infraestrutura
{
    public class PetsRepository : IPetsRepository
    {
        private readonly GerenciamentoPetShopContext _context;

        public PetsRepository(GerenciamentoPetShopContext context)
        {
            _context = context;
        }

        public void Add(Pets pets)
        {
            _context.Pets.Add(pets);
            _context.SaveChanges();
        }

        public void Delete(Pets pet)
        {
            _context.Pets.Remove(pet);
            _context.SaveChanges();
        }

        public List<Pets> Get(int pageNumber, int pageQuantity)
        {
            if (pageNumber < 1 && pageQuantity < 1) 
            {
                return _context.Pets
                    .Include(p => p.Cliente)
                    .ToList();
            }
            return _context.Pets
                .Include(p => p.Cliente)
                .Skip((pageNumber - 1) * pageQuantity).Take(pageQuantity)
                .ToList();
        }

        public Pets? Get(int id)
        {
            return _context.Pets
                .Include(p => p.Cliente)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Update(Pets pet)
        {
            _context.Pets.Update(pet);
            _context.SaveChanges();
        }
    }
}
