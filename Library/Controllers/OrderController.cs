using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Library.Services.Queries;
using Library.Services.Commands;
using System.Threading;

namespace Library.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly BookStatusQueries bookStatusQueries;
        private readonly OrderCommands orderCommands;

        public OrderController(UserManager<User> userManager, BookStatusQueries bookStatusQueries, OrderCommands orderCommands)
        {
            this.userManager = userManager;
            this.bookStatusQueries = bookStatusQueries;
            this.orderCommands = orderCommands;
        }

        public async Task<IActionResult> ToBook(int id, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                await orderCommands.ToBook(user, id, cancellationToken);

                TempData["succses-alert"] = "Книга успешно забронирована!";
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MyReservations()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                var reservation = await bookStatusQueries.GetBookStatusByUserId(user.Id);

                return View(reservation);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CancelReservation(int id, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await orderCommands.CancelReservation(user, id, cancellationToken);

            TempData["succses-alert"] = "Бронирование успешно отменено!";

            return RedirectToAction("MyReservations");
        }

        public async Task<IActionResult> AllReservations()
        {
            var reservation = await bookStatusQueries.GetAllReservations();

            return View(reservation);
        }

        public async Task<IActionResult> LeaveFeedback(int bookId, int grade, string commentText, CancellationToken cancellationToken)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                await orderCommands.LeaveFeedback(user, bookId, grade, commentText, cancellationToken);
                TempData["succses-alert"] = "Отзыв успешно оставлен!";
            }

            return RedirectToAction("BookInfo", "Home", new { id = bookId });
        }
    }
}