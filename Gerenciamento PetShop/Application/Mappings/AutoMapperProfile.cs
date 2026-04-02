using AutoMapper;
using Gerenciamento_PetShop.Domain.Modelos;
using Gerenciamento_PetShop.Presentation.DTOs;

namespace Gerenciamento_PetShop.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Clientes, ClientesResponse>();
            CreateMap<Pets, PetResponse>();
        }
    }
}
