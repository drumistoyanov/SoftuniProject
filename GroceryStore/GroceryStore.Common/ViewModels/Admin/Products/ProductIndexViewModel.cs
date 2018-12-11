namespace GroceryStore.Common.ViewModels.Admin.Products
{
    public class ProductIndexViewModel
    {
        public int Id { get; set; }

        public string PictureUrl { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Weight { get; set; }
    }
}
