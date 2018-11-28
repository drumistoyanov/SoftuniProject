using System.Collections.Generic;
using System.Linq;

namespace GroceryStore.Services.ShopCart
{
    public class ShoppingCart
    {
        private readonly IList<CartItem> items;
        
        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }
        
        public IEnumerable<CartItem> Items => new List<CartItem>(this.items);

        public void AddToCart(int productId,int quantity,decimal weight)
        {
            var cartItem = this.items.SingleOrDefault(x => x.ProductId == productId && x.Weight==weight);
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Weight= weight
                };

                this.items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void RemoveFromCart(int productId,decimal weight)
        {
            var cartItem = this.items
                .FirstOrDefault(x => x.ProductId == productId && x.Weight==weight);

            if (cartItem != null)
            {
                cartItem.Quantity--;
                if (cartItem.Quantity <= 0)
                {
                    this.items.Remove(cartItem);
                }
            }
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}
