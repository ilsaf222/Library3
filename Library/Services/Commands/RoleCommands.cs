using Library.Domain.Entities;
using Library.Models.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Commands
{
    public class RoleCommands
    {
        private readonly UserManager<User> userManager;
        public RoleCommands(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task EditUserRole(User user,List<string> roles) 
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var addedRoles = roles.Except(userRoles);

            var removedRoles = userRoles.Except(roles);

            await userManager.AddToRolesAsync(user, addedRoles);

            await userManager.RemoveFromRolesAsync(user, removedRoles);
        }
    }
}
