using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Data.Models
{
    public class OrderProduct 
    {
        public int Id { get; set; }


        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public decimal ProductWeight { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal ProductPrice { get; set; }

        [Required]
        [Url]
        public string ProductPicture { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}