using Library.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Library.Services.Queries;
using Library.Services.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    [Authorize(Roles = RolesNames.AdminName)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly UserQueries userQueries;
        private readonly RoleQueries roleQueries;
        private readonly RoleCommands roleCommands;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager,
            UserQueries userQueries, RoleQueries roleQueries, RoleCommands roleCommands)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.userQueries = userQueries;
            this.roleQueries = roleQueries;
            this.roleCommands = roleCommands;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await roleQueries.GetAllRoles();

            return View(roles);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                await roleManager.CreateAsync(new IdentityRole(name));

                return RedirectToAction("Index");
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await roleManager.DeleteAsync(role);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserList()
        {
            var users = await userQueries.GetAllUsersEmails();

            return View(users);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var userRoles = await roleQueries.GetUserRoles(user);

                return View(userRoles);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {

            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                await roleCommands.EditUserRole(user, roles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
