using Library.Models.Publisher;
using Library.Services.Commands;
using Library.Services.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class PublisherController : Controller
    {
        private readonly PublishersQueries publishersQueries;

        private readonly PublisherCommands publisherCommands;

        public PublisherController(PublishersQueries publishersQueries, PublisherCommands publisherCommands)
        {
            this.publishersQueries = publishersQueries;
            this.publisherCommands = publisherCommands;
        }

        #region Publisher

        public async Task<IActionResult> IndexPublisher()
        {
            return View(await publishersQueries.GetAllPublishers());
        }

        public IActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(AddPublisherViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await publisherCommands.AddPublisher(model, cancellationToken);

                return RedirectToAction("IndexPublisher");
            }

            return View(model);
        }

        public async Task<IActionResult> EditPublisher(int id)
        {
            var publisher = await publishersQueries.GetEditPublisherById(id);

            if (publisher != null)
            {
                return View(publisher);
            }

            return RedirectToAction("IndexPublisher");
        }

        [HttpPost]
        public async Task<IActionResult> EditPublisher(EditPublisherViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await publisherCommands.EditPublisher(model, cancellationToken);

                return RedirectToAction("IndexPublisher");
            }

            return View(model);
        }

        public async Task<IActionResult> DeletePublisher(int id, CancellationToken cancellationToken)
        {
            await publisherCommands.DeletePublisher(id, cancellationToken);

            return RedirectToAction("IndexPublisher");
        }

        #endregion
    }
}
