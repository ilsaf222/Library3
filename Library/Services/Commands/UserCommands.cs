using Library.Domain.Entities;
using Library.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Library.Services.Commands
{
    public class UserCommands
    {
        private readonly UserManager<User> userManager;

        public UserCommands(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateNewUser(CreateUserViewModel model)
        {
            var user = new User()
            {
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            await userManager.CreateAsync(user, model.Password);
        }

        public async Task EditUser(User user, EditUserViewModel model)
        {
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;

            await userManager.UpdateAsync(user);
        }
    }
}
