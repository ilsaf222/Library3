using System.ComponentModel.DataAnnotations;

namespace Library.Models.Publisher
{
    public class ListPublisherViewModel
    {
        public int Id { get; set; }

        [Display(Name="Издатель")]
        public string Name { get; set; }
    }
}
