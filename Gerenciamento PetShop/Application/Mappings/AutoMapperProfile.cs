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

            CreateMap<Pets, PetUpdateResponse>()
            .ForMember(dest => dest.TipoAnimal,
                       opt => opt.MapFrom(src => src.TipoAnimal.ToString())) // Converte "1" para "Cachorro"
            .ForMember(dest => dest.PorteAnimal,
                       opt => opt.MapFrom(src => src.PorteAnimal.ToString()));

            CreateMap<Pets, PetResponse>()
            .ForMember(dest => dest.NomeCliente,
                       opt => opt.MapFrom(src => src.Cliente.Nome))
            .ForMember(dest => dest.ClienteId,
                       opt => opt.MapFrom(src => src.Cliente.Id))
            .ForMember(dest => dest.TipoAnimal,
                       opt => opt.MapFrom(src => src.TipoAnimal.ToString())) // Converte "1" para "Cachorro"
            .ForMember(dest => dest.PorteAnimal,
                       opt => opt.MapFrom(src => src.PorteAnimal.ToString()));
        }
    }
}
