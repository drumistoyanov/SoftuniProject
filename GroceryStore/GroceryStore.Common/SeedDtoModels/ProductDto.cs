using GroceryStore.Data.Models;

namespace GroceryStore.Common.SeedDtoModels
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public string Type { get; set; }

        public string Kind { get; set; }

        public decimal Weight { get; set; }

        public decimal Discount { get; set; }

        public decimal Price { get; set; }

        public string ManufacturerName { get; set; }
    }
}
