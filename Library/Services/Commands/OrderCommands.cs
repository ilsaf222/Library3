using Library.Domain;
using Library.Domain.Entities;
using Library.Domain.Models;
using Library.EmailServices;
using Library.Models.Book;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Services.Commands
{
    public class OrderCommands
    {
        private readonly IRepository<BookStatus> repository;
        private readonly IRepository<BookReview> repositoryReview;
        private readonly IConfiguration configuration;

        public OrderCommands(IRepository<BookStatus> repository, IRepository<BookReview> repositoryReview, IConfiguration configuration)
        {
            this.repository = repository;
            this.repositoryReview = repositoryReview;
            this.configuration = configuration;
        }

        public async Task ToBook(User user, int bookId, CancellationToken cancellationToken)
        {
            var getBook = await repository.GetAll()
                    .Where(x => x.BookId == bookId)
                    .FirstOrDefaultAsync();

            if (getBook.Status == Status.Avaliable)
            {
                getBook.Status = Status.Booked;
                getBook.UserId = user.Id;
                getBook.LastTime = DateTime.Now;

                await repository.UpdateAsync(getBook, cancellationToken);

                try
                {
                    var addressTo = new List<EmailAddress>
                    {
                        new EmailAddress() { Address = user.Email, Name = user.UserName }
                    };

                    var email = new EmailSender(configuration);
                    await email.SendMessageAsync(addressTo, "Информация!",
                    $"Книга забронирована");
                }
                catch
                {

                }
            }
        }

        public async Task CancelReservation(User user, int id, CancellationToken cancellationToken)
        {
            var status = await repository.GetByIdAsync(id, cancellationToken);

            if (status != null && status.Status == Status.Booked)
            {
                status.Status = Status.Avaliable;
                status.UserId = null;

                await repository.UpdateAsync(status, cancellationToken);

                if (user != null)
                {
                    var addressTo = new List<EmailAddress>
                    {
                        new EmailAddress() { Address = user.Email, Name = user.UserName }
                    };

                    try
                    {
                        var email = new EmailSender(configuration);
                        await email.SendMessageAsync(addressTo, "Информация!",
                        $"Бронирование книги отменено!");
                    }
                    catch
                    {
                    }
                }
            }
        }

        public async Task LeaveFeedback(User user, int bookId, int grade, string commentText, CancellationToken cancellationToken)
        {
            var review = await repositoryReview.GetAll()
                    .FirstOrDefaultAsync(x => x.BookId == bookId);

            if (review == null)
            {
                var newReaview = new BookReview()
                {
                    BookId = bookId,
                };

                await repositoryReview.AddAsync(newReaview, cancellationToken);

                review = await repositoryReview.GetAll()
                .FirstOrDefaultAsync(x => x.BookId == bookId);
            }

            List<JsonCommentConfiguration> commentList = null;

            if (review.CommentsString == null)
            {
                commentList = new List<JsonCommentConfiguration>();

                commentList.Add(new JsonCommentConfiguration()
                {
                    CommentText = commentText,
                    Grade = (byte)grade,
                    UserId = user.Id,
                });
            }

            if (review.CommentsString != null)
            {
                commentList = JsonConvert.DeserializeObject<List<JsonCommentConfiguration>>(review.CommentsString);

                var findUser = commentList.Find(x => x.UserId == user.Id);

                if (findUser == null)
                {
                    commentList.Add(new JsonCommentConfiguration()
                    {
                        CommentText = commentText,
                        Grade = (byte)grade,
                        UserId = user.Id,
                    });
                }
            }

            var serobject = JsonConvert.SerializeObject(commentList);

            review.CommentsString = serobject;

            await repositoryReview.UpdateAsync(review, cancellationToken);
        }
    }
}
