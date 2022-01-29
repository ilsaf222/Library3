using Library.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Order
{
    public class ListResevationViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        [Display(Name="Название")]
        public string Name { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Display(Name = "Издатель")]
        public string Publisher { get; set; }

        [Display(Name = "Статус")]
        public Status Status { get; set; }
    }
}
