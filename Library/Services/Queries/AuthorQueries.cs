using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Author;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Queries
{
    public class AuthorQueries
    {
        private readonly IRepository<Author> repository;
        private readonly IMapper mapper;

        public AuthorQueries(IRepository<Author> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ListAuthorViewModel>> GetAllAuthors()
        {
            return await repository.GetAll()
                .ProjectTo<ListAuthorViewModel>(mapper.ConfigurationProvider)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<EditAuthorViewModel> GetEditAuthorById(int id)
        {
            return await repository.GetAll()
                .ProjectTo<EditAuthorViewModel>(mapper.ConfigurationProvider)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
