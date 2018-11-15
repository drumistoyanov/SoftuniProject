namespace GroceryStore.Common.ViewModels.ShoppingCart
{
    public class ProductCartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Weight { get; set; }

        public int Quantity { get; set; }
    }
}
