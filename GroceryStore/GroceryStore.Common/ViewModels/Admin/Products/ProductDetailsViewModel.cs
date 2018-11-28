﻿using GroceryStore.Data.Models;
using System.Collections.Generic;
using System.Net.Mime;

namespace GroceryStore.Common.ViewModels.Admin.Products
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string PictureUrl { get; set; }
        
        public string Type { get; set; }

        public Kind Kind { get; set; }  

        public decimal Weight { get; set; }
        
        public decimal Discount { get; set; }
        
        public decimal Price { get; set; }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        
        public ICollection<Image> Images { get; set; }
    }
}
