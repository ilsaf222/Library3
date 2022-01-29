using System.ComponentModel.DataAnnotations;

namespace Library.Models.Publisher
{
    public class AddPublisherViewModel
    {
        [Display(Name = "Издатель")]
        public string Name { get; set; }
    }
}
