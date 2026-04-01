using Gerenciamento_PetShop.Application.Interfaces;
using Gerenciamento_PetShop.Domain.Interfaces;
using Gerenciamento_PetShop.Domain.Modelos;
using AutoMapper;
using Gerenciamento_PetShop.Presentation.DTOs;
using Gerenciamento_PetShop.Presentation.ViewModel;

namespace Gerenciamento_PetShop.Application.Services
{
    public class PetsService : IPetsService
    {
        public readonly IPetsRepository _petsRepository;
        private readonly IMapper _mapper;
        public PetsService(IPetsRepository petsRepository, IMapper mapper)
        {
            _petsRepository = petsRepository;
            _mapper = mapper;
        }

        public void AdicionarPet(PetsCreateViewModel petsViewModel)
        {
            var pet = new Pets();
            pet = new Pets(petsViewModel.Nome, petsViewModel.TipoAnimal, petsViewModel.PorteAnimal, petsViewModel.ClienteId);
            _petsRepository.Add(pet);
        }

        public List<PetResponse> GetPets(int pageNumber, int pageQuantity)
        {
           var pets = _petsRepository.Get(pageNumber, pageQuantity);
            return _mapper.Map<List<PetResponse>>(pets);
        }

        public PetResponse GetPetById(int id)
        {
            var pet = _petsRepository.Get(id);
            if (pet == null) return null;
            return _mapper.Map<PetResponse>(pet); ;
        }

        public PetResponse AtualizarPet(int id, PetsUpdateViewModel petsViewModel)
        {
            var pet = _petsRepository.Get(id);
            if (pet == null) return null;
            if (petsViewModel.Nome != null) pet.Nome = petsViewModel.Nome;
            if (petsViewModel.TipoAnimal != 0) pet.TipoAnimal = petsViewModel.TipoAnimal;
            if (petsViewModel.PorteAnimal != 0) pet.PorteAnimal = petsViewModel.PorteAnimal;
            if (petsViewModel.ClienteId != 0) pet.ClienteId = petsViewModel.ClienteId;
            _petsRepository.Update(pet);
            return _mapper.Map<PetResponse>(pet);
        }

        public bool DeletarPet(int id)
        {
            var cliente = _petsRepository.Get(id);
            if (cliente == null) return false;
            _petsRepository.Delete(cliente);
            return true;
        }
    }
}
