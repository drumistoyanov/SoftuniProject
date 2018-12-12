using System.Collections.Generic;
using System.Linq;

namespace GroceryStore.Services.ShopCart
{
    public class ShoppingCart
    {
        private readonly IList<CartItem> _items;
        
        public ShoppingCart()
        {
            _items = new List<CartItem>();
        }
        
        public IEnumerable<CartItem> Items => new List<CartItem>(_items);

        public void AddToCart(int productId,int quantity)
        {
            var cartItem = _items.SingleOrDefault(x => x.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };

                _items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cartItem = _items
                .FirstOrDefault(x => x.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity--;
                if (cartItem.Quantity <= 0)
                {
                    _items.Remove(cartItem);
                }
            }
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
