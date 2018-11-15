using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data.Models;

namespace GroceryStore.Common.ViewModels
{
    public class TypesProductsViewModel
    {
        public IEnumerable<AllTypesViewModel> Types { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }

        public IEnumerable<ProductIndexViewModel> Products { get; set; }

        public PagesViewModel Page { get; set; }

        public string Type { get; set; }
    }
}
