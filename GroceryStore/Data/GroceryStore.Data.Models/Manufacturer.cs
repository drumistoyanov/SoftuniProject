using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GroceryStore.Data.Common.Models;

namespace GroceryStore.Data.Models
{
    public class Manufacturer : BaseModel<int>
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }

        public string City { get; set; }

        [Required]
        [Url]
        public string LogoUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
