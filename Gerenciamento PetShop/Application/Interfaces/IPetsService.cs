using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Interfaces
{
    public interface IPetsService
    {
        void AdicionarPet (PetsCreateViewModel petsViewModel);
        List<PetResponse> GetPets(int pageNumber, int pageQuantity);
        PetResponse GetPetById(int id);
        PetUpdateResponse AtualizarPet (int id, PetsUpdateViewModel petsViewModel);
        bool DeletarPet(int id);
    }
}
