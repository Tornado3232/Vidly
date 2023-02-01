using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, MembershipType>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
        }
    }
}
