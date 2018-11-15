using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Data.Models;

namespace GroceryStore.Common.ViewModels
{
    public class ManufacturersProductsViewModel
    {
        public IEnumerable<Manufacturer> Manufacturers { get; set; }

        public ManufacturerDetailsViewModel ManufacturerWithProducts{ get; set; }

        public PagesViewModel Page { get; set; }
    }
}
