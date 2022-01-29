using System.ComponentModel.DataAnnotations;

namespace Library.Models.Author
{
    public class AddAuthorViewModel
    {
        [Display(Name = "Автор")]
        public string Name { get; set; }
    }
}
