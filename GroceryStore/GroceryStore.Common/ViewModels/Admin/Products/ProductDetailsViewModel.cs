using System.Collections.Generic;
using GroceryStore.Data.Models;

namespace GroceryStore.Common.ViewModels.Admin.Products
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string PictureUrl { get; set; }
        
        public string Type { get; set; }

        public string Kind { get; set; }  

        public decimal Weight { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal Price { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        
        public ICollection<Image> Images { get; set; }
    }
}
