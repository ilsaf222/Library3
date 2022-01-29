using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Domain;
using Library.Domain.Entities;
using Library.Models;
using Library.Models.Book;
using Library.Services.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<BookReview> repositoryBookReview;

        private readonly BookQueries bookQueries;

        public HomeController(IRepository<BookReview> repositoryBookReview, BookQueries bookQueries)
        {
            this.repositoryBookReview = repositoryBookReview;
            this.bookQueries = bookQueries;
        }

        public async Task<IActionResult> Index(string sortsValue, string findsValue, string findText)    
        {

            var books = await bookQueries.GetAllBooks(sortsValue, findsValue, findText);

            ViewBag.FindText = findText;

            return View(books);
        }

        public async Task<IActionResult> BookInfo(int id)  
        {
            var book = await bookQueries.GetBookById(id);

            var reviews = await repositoryBookReview.GetAll()
                .FirstOrDefaultAsync(x => x.BookId == id);

            if (reviews != null)
            {
                ViewBag.Reviews = await bookQueries.GetBookAllComments(reviews.CommentsString);
            }

            return View(book);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
