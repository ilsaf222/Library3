using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Library.Models.Book
{
    public class AddBookViewModel
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Выберите автора")]
        public int AuthorId { get; set; }

        [Display(Name = "Выберите жанр")]
        public int GenreId { get; set; }

        [Display(Name = "Выберите издателя")]
        public int PublisherId { get; set; }

        [Display(Name = "Фото")]
        public IFormFile BookImg { get; set; } 
    }
}