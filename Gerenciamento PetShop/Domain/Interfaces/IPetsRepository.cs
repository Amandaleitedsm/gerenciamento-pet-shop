using Gerenciamento_PetShop.Domain.Modelos;

namespace Gerenciamento_PetShop.Domain.Interfaces
{
    public interface IPetsRepository
    {
        void Add(Pets pets);
        List<Pets> Get(int pageNumber, int pageQuantity);
        Pets? Get(int id);
        void Update(Pets pet);
        void Delete(Pets pet);
    }
}
