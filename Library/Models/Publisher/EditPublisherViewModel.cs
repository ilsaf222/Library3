using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Publisher
{
    public class EditPublisherViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Издатель")]
        public string Name { get; set; }
    }
}
