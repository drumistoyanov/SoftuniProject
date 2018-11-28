using System.Collections.Generic;

namespace GroceryStore.Services.ShopCart.Interfaces
{
    public interface IShoppingCartManager
    {
        void AddToCart(string id, int productId, int quantity, decimal weight);
            
        void RemoveFromCart(string id,int productId, decimal weight);

        IEnumerable<CartItem> GetItems(string id);

        void Clear(string id);
    }
}
