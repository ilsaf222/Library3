using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Genre;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.Commands
{
    public class GenreCommands
    {
        private readonly IRepository<Genre> repository;

        public GenreCommands(IRepository<Genre> repository)
        {
            this.repository = repository;
        }

        public async Task AddGenre(AddGenreViewModel model, CancellationToken cancellationToken)
        {
            var genre = new Genre()
            {
                Name = model.Name,
            };

            await repository.AddAsync(genre, cancellationToken);
        }

        public async Task EditGenre(EditGenreViewModel model, CancellationToken cancellationToken)
        {
            var genre = await repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (genre != null)
            {
                genre.Name = model.Name;

                await repository.UpdateAsync(genre, cancellationToken);
            }
        }

        public async Task DeleteGenre(int id, CancellationToken cancellationToken)
        {
            var genre = await repository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (genre != null)
            {
                await repository.RemoveAsync(genre, cancellationToken);
            }
        }

    }
}
