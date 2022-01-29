using AutoMapper;
using Library.Models.Role;
using Microsoft.AspNetCore.Identity;

namespace Library.MapperProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, ListRoleViewModel>();
        }
    }
}
