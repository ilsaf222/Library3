using AutoMapper;
using Library.Domain.Entities;
using Library.Models.Genre;

namespace Library.MapperProfile
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, ListGenreViewModel>();
            CreateMap<Genre, EditGenreViewModel>();
        }
    }
}
