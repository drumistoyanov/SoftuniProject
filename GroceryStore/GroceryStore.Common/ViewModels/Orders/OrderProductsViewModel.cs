namespace GroceryStore.Common.ViewModels.Orders
{
    public class OrderProductsViewModel
    {
        public decimal ProductWeight { get; set; }
        
        public decimal ProductPrice { get; set; }

        public string ProductName { get; set; }
        
        public string ProductPicture { get; set; }

        public int Quantity { get; set; }
    }
}
