using GroceryStore.Data.Models;
using System.Collections.Generic;
using System.Net.Mime;

namespace GroceryStore.Common.ViewModels.Admin.Products
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string PictureUrl { get; set; }
        
        public string Type { get; set; }
        
        public string Brand { get; set; }
        
        public string Description { get; set; }

        public string Weight { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal Price { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        
        public ICollection<Manufacturer> Images { get; set; }
    }
}
