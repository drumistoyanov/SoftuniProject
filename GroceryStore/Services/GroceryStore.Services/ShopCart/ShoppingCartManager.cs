using System.Collections.Concurrent;
using System.Collections.Generic;
using GroceryStore.Services.ShopCart.Interfaces;

namespace GroceryStore.Services.ShopCart
{
    public class ShoppingCartManager :IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> _carts;
        
        public ShoppingCartManager() 
        {
            _carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int productId, int quantity)
        {
            var shoppingCart = GetShoppingCart(id);

            shoppingCart.AddToCart(productId, quantity);
        }
        
        public void RemoveFromCart(string id, int productId)
        {
            var shoppingCart = GetShoppingCart(id);

            shoppingCart.RemoveFromCart(productId);
        }

        public void Clear(string id)
        {
            GetShoppingCart(id).Clear();
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shoppingCart = GetShoppingCart(id);
            
            var cartItems= new List<CartItem>(shoppingCart.Items);

            return cartItems;
        }
        
        private ShoppingCart GetShoppingCart(string id)
        {
            return _carts.GetOrAdd(id, new ShoppingCart());
        }
        
    }
}
