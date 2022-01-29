using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Author
{
    public class EditAuthorViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Автор")]
        public string Name { get; set; }
    }
}
