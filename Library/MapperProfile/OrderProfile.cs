using AutoMapper;
using Library.Domain.Entities;
using Library.Models.Order;

namespace Library.MapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<BookStatus, ListResevationViewModel>()
                .ForMember(x => x.Publisher, x => x.MapFrom(p => p.Book.Publisher.Name))
                .ForMember(x => x.Genre, x => x.MapFrom(p => p.Book.Genre.Name))
                .ForMember(x => x.Author, x => x.MapFrom(p => p.Book.Author.Name))
                .ForMember(x => x.Name, x => x.MapFrom(p => p.Book.Name))
                .ForMember(x => x.BookId, x => x.MapFrom(p => p.BookId));

        }
    }
}
