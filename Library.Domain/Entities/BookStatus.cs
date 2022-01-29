using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
    public class BookStatus
    {
        [Key]
        public int Id { get; set; }

        public int? BookId { get; set; }

        [Required]
        public Book Book { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public Status Status { get; set; }

        public DateTime LastTime { get; set; }

    }
}
