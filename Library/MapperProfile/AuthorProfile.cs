using AutoMapper;
using Library.Domain.Entities;
using Library.Models.Author;

namespace Library.MapperProfile
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, ListAuthorViewModel>();
            CreateMap<Author, EditAuthorViewModel>();
        }
    }
}
