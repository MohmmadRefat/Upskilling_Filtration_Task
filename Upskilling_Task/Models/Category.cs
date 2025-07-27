using System.ComponentModel.DataAnnotations;

namespace Upskilling_Task.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public IList<Book> Books { get; set; } = new List<Book>();
    }
}