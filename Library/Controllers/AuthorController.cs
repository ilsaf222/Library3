using Library.Domain.Entities;
using Library.Models.Author;
using Library.Services.Commands;
using Library.Services.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize(Roles = RolesNames.LibrarianName)]
    public class AuthorController : Controller
    {
        private readonly AuthorQueries authorQueries;
        private readonly AuthorCommands authorCommands;

        public AuthorController(AuthorQueries authorQueries, AuthorCommands authorCommands)
        {
            this.authorQueries = authorQueries;
            this.authorCommands = authorCommands;
        }

        #region AUTHOR

        public async Task<IActionResult> IndexAuthor()
        {
            return View(await authorQueries.GetAllAuthors());
        }

        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await authorCommands.AddAuthor(model, cancellationToken);

                return RedirectToAction("IndexAuthor");
            }

            return View(model);
        }

        public async Task<IActionResult> EditAuthor(int id)
        {
            var author = await authorQueries.GetEditAuthorById(id);

            if (author != null)
            {
                return View(author);
            }
            return RedirectToAction("IndexAuthor");
        }

        [HttpPost]
        public async Task<IActionResult> EditAuthor(EditAuthorViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await authorCommands.EditAuthor(model, cancellationToken);

                return RedirectToAction("IndexAuthor");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            await authorCommands.DeleteAuthor(id, cancellationToken);

            return RedirectToAction("IndexGenre");
        }

        #endregion
    }
}
