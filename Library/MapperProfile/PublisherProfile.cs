using AutoMapper;
using Library.Domain.Entities;
using Library.Models.Publisher;

namespace Library.MapperProfile
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, ListPublisherViewModel>();
            CreateMap<Publisher, EditPublisherViewModel>();
        }
    }
}
