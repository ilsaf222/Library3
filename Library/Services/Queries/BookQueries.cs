using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain;
using Library.Domain.Entities;
using Library.Models.Book;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Queries
{
    public class BookQueries
    {
        private readonly IRepository<Book> repository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public BookQueries(IRepository<Book> repository, IMapper mapper, UserManager<User> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<List<ListBookViewModel>> GetAllBooks(string sortsValue, string findsValue, string findText)
        {
            var books = repository.GetAll()
                .ProjectTo<ListBookViewModel>(mapper.ConfigurationProvider);

            if (sortsValue != null && sortsValue != "null")
            {
                switch (sortsValue)
                {
                    case "name": books = books.OrderBy(x => x.Name); break;
                    case "publisher": books = books.OrderBy(x => x.Publisher); break;
                    case "genre": books = books.OrderBy(x => x.Genre); break;
                    case "author": books = books.OrderBy(x => x.Author); break;
                }
            }

            if(findsValue != null && findText != null && sortsValue != "null")
            {
                var normalizedSearchText = findText.Trim().ToUpper();

                switch (findsValue)
                {
                    case "name": books = books.Where(x => x.Name.ToUpper().Contains(normalizedSearchText)); break;
                    case "publisher": books = books.Where(x => x.Publisher.ToUpper().Contains(normalizedSearchText)); break;
                    case "genre": books = books.Where(x => x.Genre.ToUpper().Contains(normalizedSearchText)); break;
                    case "author": books = books.Where(x => x.Author.ToUpper().Contains(normalizedSearchText)); break;
                }
            }

            return await books
                .ToListAsync();
        }

        public async Task<ListBookInfoViewModel> GetBookById(int id)
        {
            return await repository.GetAll()
                .ProjectTo<ListBookInfoViewModel>(mapper.ConfigurationProvider)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CommentsList>> GetBookAllComments(string commentsString)
        {
            var deserobject = JsonConvert.DeserializeObject<List<JsonCommentConfiguration>>(commentsString);

            var commentsList = new List<CommentsList>();

            foreach (var review in deserobject)
            {
                var user = await userManager.FindByIdAsync(review.UserId);

                commentsList.Add(new CommentsList()
                {
                    CommentText = review.CommentText,
                    Grade = review.Grade,
                    Name = user.UserName
                });
            }

            return commentsList;
        }

        public async Task<EditBookViewModel> GetEditBookById(int id)
        {
            return await repository.GetAll()
                .ProjectTo<EditBookViewModel>(mapper.ConfigurationProvider)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
