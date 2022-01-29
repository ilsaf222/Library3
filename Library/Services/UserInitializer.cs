using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserInitializer
    {
        private const string AdminRoleName = RolesNames.AdminName;
        private const string AdminUserEmail = "admin@mail.ru";
        private const string AdminUserPassword = "Admin1!";

        private const string LibrarianRoleName = RolesNames.LibrarianName;
        private const string LibrarianUserEmail = "librarian@mail.ru";
        private const string LibrarianUserPassword = "Librarian1!";

        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateRole(roleManager, AdminRoleName);
            await CreateRole(roleManager, LibrarianRoleName);
            await CreateUser(userManager, AdminUserEmail, AdminUserPassword, AdminRoleName);
            await CreateUser(userManager, LibrarianUserEmail, LibrarianUserPassword, LibrarianRoleName);
        }

        private static async Task CreateUser(UserManager<User> userManager, string userEmail, string userPassword, string roleName)
        {
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                var user = new User { Email = userEmail, UserName = userEmail };

                var result = await userManager.CreateAsync(user, userPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
        private static async Task CreateRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
