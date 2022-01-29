using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Publisher;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Queries
{
    public class PublishersQueries
    {
        private readonly IRepository<Publisher> repository;
        private readonly IMapper mapper;

        public PublishersQueries(IRepository<Publisher> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ListPublisherViewModel>> GetAllPublishers()
        {
            return await repository.GetAll()
                .ProjectTo<ListPublisherViewModel>(mapper.ConfigurationProvider)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<EditPublisherViewModel> GetEditPublisherById(int id)
        {
            return await repository.GetAll()
                .ProjectTo<EditPublisherViewModel>(mapper.ConfigurationProvider)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
