using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Publisher;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.Commands
{
    public class PublisherCommands
    {
        private readonly IRepository<Publisher> repository;

        public PublisherCommands(IRepository<Publisher> repository)
        {
            this.repository = repository;
        }

        public async Task AddPublisher(AddPublisherViewModel model, CancellationToken cancellationToken)
        {
            var publisher = new Publisher()
            {
                Name = model.Name,
            };

            await repository.AddAsync(publisher, cancellationToken);
        }

        public async Task EditPublisher(EditPublisherViewModel model, CancellationToken cancellationToken)
        {
            var publisher = await repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (publisher != null)
            {
                publisher.Name = model.Name;
                await repository.UpdateAsync(publisher, cancellationToken);
            }
        }

        public async Task DeletePublisher(int id, CancellationToken cancellationToken)
        {
            var publisher = await repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (publisher != null)
            {
                await repository.RemoveAsync(publisher, cancellationToken);

            }
        }
    }
}
