using AutoMapper;
using Library.Models.Author;
using Library.Models.Genre;
using Library.Models.Publisher;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Library.Models.Book;
using Library.Services.Queries;
using Library.Services.Commands;
using Microsoft.AspNetCore.Authorization;
using Library.Domain.Entities;

namespace Library.Controllers
{
    [Authorize(Roles = RolesNames.LibrarianName)]
    public class LibrarianController : Controller
    {

        public LibrarianController()
        {

        }

        public IActionResult ControlPanel()
        {
            return View();
        }
    }
}
