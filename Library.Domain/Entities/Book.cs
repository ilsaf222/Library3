using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int? AuthorId { get; set; }

        [Required]
        public Author Author { get; set; }

        public int? GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public int? PublisherId { get; set; }

        [Required]
        public Publisher Publisher { get; set; }

        public int? BookStatusId { get; set; }

        [Required]
        public BookStatus BookStatus { get; set; }
    }
}
