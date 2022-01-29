using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Order;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Queries
{
    public class BookStatusQueries
    {
        private readonly IRepository<BookStatus> repository;
        private readonly IMapper mapper;

        public BookStatusQueries(IRepository<BookStatus> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ListResevationViewModel>> GetAllReservations()
        {
            return await repository.GetAll()
                .Where(f => f.Status != Status.Avaliable)
                .ProjectTo<ListResevationViewModel>(mapper.ConfigurationProvider)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<ListResevationViewModel>> GetBookStatusByUserId(string userId)
        {
            return await repository.GetAll()
                    .Where(x => x.UserId == userId)
                    .ProjectTo<ListResevationViewModel>(mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToListAsync();
        }
    }
}
