using AutoMapper;
using Library.Domain.Entities;
using Library.Models.Book;
using System;

namespace Library.MapperProfile
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, ListBookViewModel>()
                .ForMember(x => x.Author, x => x.MapFrom(a => a.Author.Name))
                .ForMember(x => x.Genre, x => x.MapFrom(g => g.Genre.Name))
                .ForMember(x => x.Publisher, x => x.MapFrom(p => p.Publisher.Name))
                .ForMember(x => x.Status, x => x.MapFrom(p => p.BookStatus.Status));

            CreateMap<Book, ListBookInfoViewModel>()
                .ForMember(x => x.Author, x => x.MapFrom(a => a.Author.Name))
                .ForMember(x => x.Genre, x => x.MapFrom(g => g.Genre.Name))
                .ForMember(x => x.Publisher, x => x.MapFrom(p => p.Publisher.Name))
                .ForMember(x => x.Status, x => x.MapFrom(p => p.BookStatus.Status));

            CreateMap<Book, EditBookViewModel>();
        }
    }
}
