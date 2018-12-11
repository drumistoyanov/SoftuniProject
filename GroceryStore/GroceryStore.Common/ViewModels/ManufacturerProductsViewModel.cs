using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Data.Models;

namespace GroceryStore.Common.ViewModels
{
    public class ManufacturerProductsViewModel
    {
        public IEnumerable<AllTypesViewModel> Types { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }

        public ManufacturerDetailsViewModel ManufacturerWithProducts{ get; set; }

        public PagesViewModel Page { get; set; }
    }
}
