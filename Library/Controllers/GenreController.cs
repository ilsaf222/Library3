using Library.Models.Genre;
using Library.Services.Commands;
using Library.Services.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenresQueries genresQueries;

        private readonly GenreCommands genreCommands;

        public GenreController(GenresQueries genresQueries, GenreCommands genreCommands)
        {
            this.genresQueries = genresQueries;
            this.genreCommands = genreCommands;
        }

        #region GENRE

        public async Task<IActionResult> IndexGenre()
        {
            return View(await genresQueries.GetAllGenres());
        }

        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(AddGenreViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await genreCommands.AddGenre(model, cancellationToken);

                return RedirectToAction("IndexGenre");
            }

            return View(model);
        }

        public async Task<IActionResult> EditGenre(int id)
        {
            var genre = await genresQueries.GetEditGenreById(id);

            if (genre != null)
            {
                return View(genre);
            }

            return RedirectToAction("IndexGenre");
        }

        [HttpPost]
        public async Task<IActionResult> EditGenre(EditGenreViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await genreCommands.EditGenre(model, cancellationToken);

                return RedirectToAction("IndexGenre");
            }

            return View(model);
        }


        public async Task<IActionResult> DeleteGenre(int id, CancellationToken cancellationToken)
        {
            await genreCommands.DeleteGenre(id, cancellationToken);

            return RedirectToAction("IndexGenre");
        }

        #endregion
    }
}
