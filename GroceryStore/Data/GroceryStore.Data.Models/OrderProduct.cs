using System;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Data.Models
{
    public class OrderProduct 
    {
        public int Id { get; set; }


        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal ProductPrice { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}