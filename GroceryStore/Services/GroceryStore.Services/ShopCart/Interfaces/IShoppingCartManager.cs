using System.Collections.Generic;

namespace GroceryStore.Services.ShopCart.Interfaces
{
    public interface IShoppingCartManager
    {
        void AddToCart(string id, int productId, int quantity);
            
        void RemoveFromCart(string id,int productId);

        IEnumerable<CartItem> GetItems(string id);

        void Clear(string id);
    }
}
