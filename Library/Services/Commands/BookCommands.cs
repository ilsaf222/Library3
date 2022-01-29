using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Book;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.Commands
{

    public class BookCommands
    {
        private readonly IRepository<Book> repository;
        private readonly IRepository<BookStatus> repositoryBookStatus;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BookCommands(IRepository<Book> repository, IRepository<BookStatus> repositoryBookStatus, IWebHostEnvironment hostingEnvironment)
        {
            this.repository = repository;
            this.repositoryBookStatus = repositoryBookStatus;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task AddBook(AddBookViewModel model, CancellationToken cancellationToken)
        {
            if (model.BookImg != null)
            {
                try
                {
                    string root = AppDomain.CurrentDomain.BaseDirectory;

                    string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/pictures");
                    string filePath = Path.Combine(uploads, $"{model.Name}.jpg");
                    using Stream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.BookImg.CopyToAsync(fileStream);
                }
                catch
                {
                }
            }

            var book = new Book()
            {
                AuthorId = model.AuthorId,
                GenreId = model.GenreId,
                Name = model.Name,
                PublisherId = model.PublisherId,
                ImageUrl = $"/pictures/{model.Name}.jpg"
            };

            await repository.AddAsync(book, cancellationToken);

            var getBook = await repository.GetByIdAsync(book.Id, cancellationToken);

            var status = new BookStatus()
            {
                Status = Status.Avaliable,
                LastTime = DateTime.Now,
                BookId = getBook.Id
            };

            await repositoryBookStatus.AddAsync(status, cancellationToken);

            var getStatus = await repositoryBookStatus.GetByIdAsync(status.Id, cancellationToken);

            getBook.BookStatusId = getStatus.Id;

            await repository.UpdateAsync(book, cancellationToken);
        }

        public async Task ChangeBookStatus(int id, CancellationToken cancellationToken)
        {
            var bookStatus = await repositoryBookStatus.GetAll()
                .Where(x => x.BookId == id)
                .FirstOrDefaultAsync();

            if (bookStatus != null)
            {
                if (bookStatus.Status != Status.Avaliable)
                {
                    switch (bookStatus.Status)
                    {
                        case Status.Booked: bookStatus.Status = Status.Gived; break;

                        case Status.Gived:
                            bookStatus.Status = Status.Avaliable;
                            bookStatus.UserId = null;
                            break;
                    }

                    await repositoryBookStatus.UpdateAsync(bookStatus, cancellationToken);
                }
            }
        }

        public async Task EditBook(EditBookViewModel model, CancellationToken cancellationToken)
        {
            var getBook = await repository.GetByIdAsync(model.Id, cancellationToken);

            if (model.BookImg != null)
            {
                try
                {
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "wwwroot\\pictures");
                    string filePath = Path.Combine(uploads, $"{model.BookImg.FileName}.jpg");
                    using Stream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.BookImg.CopyToAsync(fileStream);

                    getBook.ImageUrl = $"/pictures/{model.Name}.jpg";
                }
                catch
                {
                }
            }

            getBook.AuthorId = model.AuthorId;
            getBook.GenreId = model.GenreId;
            getBook.Name = model.Name;
            getBook.PublisherId = model.PublisherId;

            await repository.UpdateAsync(getBook, cancellationToken);
        }

        public async Task DeleteBook(int id, CancellationToken cancellationToken)
        {
            var getBook = await repository.GetByIdAsync(id, cancellationToken);

            await repository.RemoveAsync(getBook, cancellationToken);
        }
    }
}
