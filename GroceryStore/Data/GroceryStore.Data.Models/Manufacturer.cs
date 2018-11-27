using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Data.Models
{
    public class Manufacturer 
    {
        public Manufacturer()
        {
            this.Products=new HashSet<Product>();

        }
        public int Id { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }

        public string City { get; set; }

        [Required]
        [Url]
        public string LogoUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; } 
    }
}
