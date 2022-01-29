using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Genre;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Queries
{
    public class GenresQueries
    {
        private readonly IRepository<Genre> repository;
        private readonly IMapper mapper;

        public GenresQueries(IRepository<Genre> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ListGenreViewModel>> GetAllGenres()
        {
            return await repository.GetAll()
            .ProjectTo<ListGenreViewModel>(mapper.ConfigurationProvider)
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<EditGenreViewModel> GetEditGenreById(int id)
        {
            return await repository.GetAll()
                .ProjectTo<EditGenreViewModel>(mapper.ConfigurationProvider)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
