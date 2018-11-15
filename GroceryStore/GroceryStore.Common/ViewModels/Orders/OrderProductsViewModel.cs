namespace GroceryStore.Common.ViewModels.Orders
{
    public class OrderProductsViewModel
    {
        public string ProductWeight { get; set; }
        
        public decimal ProductPrice { get; set; }

        public string ProductTitle { get; set; }
        
        public string ProductPicture { get; set; }

        public int Quantity { get; set; }
    }
}
