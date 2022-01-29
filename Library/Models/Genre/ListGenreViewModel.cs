using System.ComponentModel.DataAnnotations;

namespace Library.Models.Genre
{
    public class ListGenreViewModel
    {
        public int Id { get; set; }

        [Display(Name="Жанр")]
        public string Name { get; set; }
    }
}
