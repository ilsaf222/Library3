using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class BookReview 
    {
        [Key]
        public int Id { get; set; }

        public int? BookId { get; set; }

        [Required]
        public Book Book { get; set; }

        [Column(TypeName = "jsonb")]
        public string CommentsString { get; set; }
    }
}
