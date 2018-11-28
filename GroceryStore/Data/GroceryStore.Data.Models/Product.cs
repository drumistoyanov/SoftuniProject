using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication.ExtendedProtection;


namespace GroceryStore.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
        }

        public int Id { get; set; }


        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        [Required]
        public string Kind { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Weight { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Discount { get; set; }

        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        public int ManufacturerId { get; set; }
        
        public Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
