using System.ComponentModel.DataAnnotations;

namespace Library.Models.Author
{
    public class ListAuthorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Автор")]
        public string Name { get; set; }
    }
}
