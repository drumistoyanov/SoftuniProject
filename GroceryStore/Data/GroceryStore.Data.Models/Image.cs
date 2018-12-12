using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Data.Models
{
    public class Image 
    {
        public int Id { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
