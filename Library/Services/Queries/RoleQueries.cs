using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain.Entities;
using Library.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Queries
{
    public class RoleQueries
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RoleQueries(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<List<ListRoleViewModel>> GetAllRoles()
        {
            return await roleManager.Roles
                .ProjectTo<ListRoleViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ChangeRoleViewModel> GetUserRoles(User user)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            var allRoles = await roleManager.Roles.ToListAsync();

            var model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };

            return model;
        }
    }
}
