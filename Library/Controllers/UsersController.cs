using AutoMapper;
using Library.Domain.Entities;
using Library.Models.User;
using Library.Services.Commands;
using Library.Services.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize(Roles = RolesNames.AdminName)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        private readonly UserCommands userCommands;
        private readonly UserQueries userQueries;
        public UsersController(UserManager<User> userManager, IMapper mapper, UserCommands userCommands, UserQueries userQueries)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.userCommands = userCommands;
            this.userQueries = userQueries;
        }

        public async Task<IActionResult> Index()
        {
            var users = await userQueries.GetAllUsersFullInfo();

            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await userCommands.CreateNewUser(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            
            var model = new EditUserViewModel { Id = user.Id, Email = user.Email, UserName = user.Email, PhoneNumber = user.PhoneNumber };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    await userCommands.EditUser(user, model);

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ConfirmUserEmail(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    var result =
                        await _passwordValidator.ValidateAsync(userManager, user, model.NewPassword);

                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await userManager.UpdateAsync(user);
                    }

                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }
    }
}
