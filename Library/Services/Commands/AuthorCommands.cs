using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Author;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.Commands
{
    public class AuthorCommands
    {
        private readonly IRepository<Author> repository;

        public AuthorCommands(IRepository<Author> repository)
        {
            this.repository = repository;
        }

        public async Task AddAuthor(AddAuthorViewModel model, CancellationToken cancellationToken)
        {
            var author = new Author()
            {
                Name = model.Name,
            };

            await repository.AddAsync(author, cancellationToken);
        }

        public async Task EditAuthor(EditAuthorViewModel model, CancellationToken cancellationToken)
        {
            var author = await repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (author != null)
            {
                author.Name = model.Name;

                await repository.UpdateAsync(author, cancellationToken);
            }
        }

        public async Task DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            var author = await repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (author != null)
            {
                await repository.RemoveAsync(author, cancellationToken);

            }
        }

    }
}
