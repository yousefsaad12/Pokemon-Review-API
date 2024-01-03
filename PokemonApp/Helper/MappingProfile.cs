using AutoMapper;
using PokemonApp.Dto;
using PokemonApp.Models;

namespace PokemonApp.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pokemon,PokemonDTO>();
            CreateMap<PokemonDTO, Pokemon>();
            CreateMap<Category,CategoryDTO>();
            CreateMap<CategoryDTO,Category>();
            CreateMap<Country,CountryDTO>();
            CreateMap<CountryDTO,Country>();
            CreateMap<Owner,OwnerDTO>();
            CreateMap<OwnerDTO,Owner>();
            CreateMap<Review,ReviewDTO>();
            CreateMap<ReviewDTO,Review>();
            CreateMap<Reviewer,ReviewerDTO>();
            CreateMap<ReviewerDTO,Reviewer>();

        }
    }
}
