﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using GroceryStore.Services.ShopCart.Interfaces;

namespace GroceryStore.Services.ShopCart
{
    public class ShoppingCartManager :IShoppingCartManager
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;
        
        public ShoppingCartManager() 
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int productId, int quantity, decimal weight)
        {
            var shopingCart = this.GetShoppingCart(id);

            shopingCart.AddToCart(productId, quantity,weight);
        }
        
        public void RemoveFromCart(string id, int productId, decimal weight)
        {
            var shopingCart = this.GetShoppingCart(id);

            shopingCart.RemoveFromCart(productId,weight);
        }

        public void Clear(string id)
        {
            this.GetShoppingCart(id).Clear();
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shopingCart = this.GetShoppingCart(id);
            
            var cartItems= new List<CartItem>(shopingCart.Items);

            return cartItems;
        }
        
        private ShoppingCart GetShoppingCart(string id)
        {
            return this.carts.GetOrAdd(id, new ShoppingCart());
        }
        
    }
}
