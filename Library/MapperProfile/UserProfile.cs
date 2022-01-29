using AutoMapper;
using Library.Domain.Entities;
using Library.Models.User;

namespace Library.MapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LIstUserViewModel>();
            CreateMap<User, ListUserFullInfoViewModel>();
        }
    }
}
