using Library.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.DataBase
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(TEntity item, CancellationToken cancellationToken)
        {
            dbSet.Add(item);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> items, CancellationToken cancellationToken)
        {
            dbSet.AddRange(items);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(TEntity item, CancellationToken cancellationToken)
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity item, CancellationToken cancellationToken)
        {
            dbSet.Update(item);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
