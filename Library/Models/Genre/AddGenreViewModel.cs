using System.ComponentModel.DataAnnotations;

namespace Library.Models.Genre
{
    public class AddGenreViewModel
    {
        [Display(Name = "Жанр")]
        public string Name { get; set; }
    }
}
