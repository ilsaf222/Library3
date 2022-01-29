using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Book
{
    public class EditBookViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

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
