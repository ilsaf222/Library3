using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Genre
{
    public class EditGenreViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Жанр")]
        public string Name { get; set; }
    }
}
