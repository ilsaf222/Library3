using Library.Models.Book;
using Library.Services.Commands;
using Library.Services.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly BookQueries bookQueries;
        private readonly AuthorQueries authorQueries;
        private readonly GenresQueries genresQueries;
        private readonly PublishersQueries publishersQueries;

        private readonly BookCommands bookCommands;

        public BookController(BookQueries bookQueries, BookCommands bookCommands,
            AuthorQueries authorQueries, GenresQueries genresQueries, PublishersQueries publishersQueries)
        {
            this.bookQueries = bookQueries;
            this.authorQueries = authorQueries;
            this.genresQueries = genresQueries;
            this.publishersQueries = publishersQueries;
            this.bookCommands = bookCommands;
        }

        public async Task<IActionResult> AddBook()
        {
            ViewBag.Authors = await authorQueries.GetAllAuthors();

            ViewBag.Genres = await genresQueries.GetAllGenres();

            ViewBag.Publishers = await publishersQueries.GetAllPublishers();

            return View(new AddBookViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await bookCommands.AddBook(model, cancellationToken);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> EditBook(int id)
        {
            ViewBag.Authors = await authorQueries.GetAllAuthors();

            ViewBag.Genres = await genresQueries.GetAllGenres();

            ViewBag.Publishers = await publishersQueries.GetAllPublishers();

            var model = await bookQueries.GetEditBookById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(EditBookViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await bookCommands.EditBook(model, cancellationToken);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            await bookCommands.DeleteBook(id, cancellationToken);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetBook(int id, CancellationToken cancellationToken)
        {
            await bookCommands.ChangeBookStatus(id, cancellationToken);

            return RedirectToAction("AllReservations", "Order");
        }
    }
}
