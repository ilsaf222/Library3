using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain.Entities;
using Library.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Queries
{
    public class UserQueries
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserQueries(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<ListUserFullInfoViewModel>> GetAllUsersFullInfo()
        {
            return await mapper
                .ProjectTo<ListUserFullInfoViewModel>(userManager.Users)
                .ToListAsync();
        }

        public async Task<List<LIstUserViewModel>> GetAllUsersEmails()
        {
            return await mapper
                .ProjectTo<LIstUserViewModel>(userManager.Users)
                .ToListAsync();

        }
    }
}
