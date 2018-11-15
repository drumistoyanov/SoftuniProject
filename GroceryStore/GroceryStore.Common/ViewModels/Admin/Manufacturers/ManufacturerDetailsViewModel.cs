using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Admin.Products;

namespace GroceryStore.Common.ViewModels.Admin.Manufacturers
{
    public class ManufacturerDetailsViewModel
    {
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public IEnumerable<ProductIndexViewModel> Products { get; set; }
    }
}
