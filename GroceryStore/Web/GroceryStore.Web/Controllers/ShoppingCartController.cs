using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GroceryStore.Common.Constants;
using GroceryStore.Common.ViewModels.ShoppingCart;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.ShopCart.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartManager _shoppingCartManager;
        private readonly GroceryStoreDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager,
            GroceryStoreDbContext dbContext,
            UserManager<User> userManager)
        {
            this._shoppingCartManager = shoppingCartManager;
            this._dbContext = dbContext;
            this._userManager = userManager;
        }

        public IActionResult Items()
        {
            var shoppingCartId = this.GetStoppingCartId();

            var model = this.GetItemsModel(shoppingCartId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FinishOrder()
        {
            var shoppingCartId = this.GetStoppingCartId();

            var items = this.GetItemsModel(shoppingCartId);
            var userId = this._userManager.GetUserId(User);
            var order = new Order
            {
                UserId = userId,
                TotalPrice = items.Sum(x => x.Price * x.Quantity),
                DateOfOrdering = DateTime.Now
            };

            foreach (var item in items)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    Order = order,
                    ProductPrice = item.Price,
                    ProductName = item.Name,
                    ProductPicture = item.PictureUrl,
                    Quantity = item.Quantity,
                    ProductId = item.Id,
                    ProductWeight = item.Weight*item.Quantity,
                });
            }

            this._dbContext.Add(order);
            this._dbContext.SaveChanges();

            this._shoppingCartManager.Clear(shoppingCartId);

            return RedirectToPage(ControllersConstants.RedirectToOrdersSuccessful);
        }

        public IActionResult AddToCart(int id, int quantity)
        {
            var shoppingCartId = this.GetStoppingCartId();
            this._shoppingCartManager.AddToCart(shoppingCartId, id, quantity);

            return RedirectToAction(nameof(Items));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCartId = this.GetStoppingCartId();
            this._shoppingCartManager.RemoveFromCart(shoppingCartId, id);

            return RedirectToAction(nameof(Items));
        }

        private string GetStoppingCartId()
        {
            var shoppingCartId = this.HttpContext.Session.GetString(ControllersConstants.ShoppingCartId);

            if (shoppingCartId == null)
            {
                shoppingCartId = Guid.NewGuid().ToString();
                this.HttpContext.Session.SetString(ControllersConstants.ShoppingCartId, shoppingCartId);
            }

            return shoppingCartId;
        }

        private List<ProductCartViewModel> GetItemsModel(string shoppingCartId)
        {
            var cartItems = this._shoppingCartManager.GetItems(shoppingCartId).ToList();

            var itemIds = cartItems.Select(i => i.ProductId);

            var products = new List<Product>();

            var cartViewModels=new List<ProductCartViewModel>();

            foreach (var id in itemIds)
            {
                var product = this._dbContext.Products.SingleOrDefault(x => x.Id == id);
                products.Add(product);
            }
            foreach (var product in products)
            {
                var model1 = new ProductCartViewModel()
                {
                    Discount = product.Discount,
                    Id = product.Id,
                    Name = product.Name,
                    PictureUrl = product.PictureUrl,
                    Price = product.Price,
                    Quantity =0 ,
                    Weight = product.Weight
                };
                cartViewModels.Add(model1);
            }
            
            for (int i = 0; i < cartViewModels.Count(); i++)
            {
                cartViewModels[i].Quantity = cartItems[i].Quantity;
                cartViewModels[i].Weight = products[i].Weight*cartItems[i].Quantity;
                cartViewModels[i].Price = Math.Round(cartViewModels[i].Price - ((cartViewModels[i].Price * cartViewModels[i].Discount) / 100), 2);
            }
          

            return cartViewModels;
        }
    }
}