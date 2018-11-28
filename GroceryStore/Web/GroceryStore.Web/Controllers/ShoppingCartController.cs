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
        private readonly IShoppingCartManager shoppingCartManager;
        private readonly GroceryStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager,
            GroceryStoreDbContext dbContext,
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.shoppingCartManager = shoppingCartManager;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
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
            var userId = this.userManager.GetUserId(User);
            var order = new Order
            {
                UserId = userId,
                TotalPrice = items.Sum(x => x.Price * x.Quantity),
                DateOfOrdering = DateTime.UtcNow
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
                    ProductWeight = item.Weight,
                });
            }

            this.dbContext.Add(order);
            this.dbContext.SaveChanges();

            this.shoppingCartManager.Clear(shoppingCartId);

            return RedirectToPage(ControllersConstants.RedirectToOrdersSuccessful);
        }

        public IActionResult AddToCart(int id, int quantity, decimal weight)
        {
            var shoppingCartId = this.GetStoppingCartId();
            this.shoppingCartManager.AddToCart(shoppingCartId, id, quantity, weight);

            return RedirectToAction(nameof(Items));
        }

        public IActionResult RemoveFromCart(int id, decimal weight)
        {
            var shoppingCartId = this.GetStoppingCartId();
            this.shoppingCartManager.RemoveFromCart(shoppingCartId, id, weight);

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
            var cartItems = this.shoppingCartManager.GetItems(shoppingCartId).ToList();

            var itemIds = cartItems.Select(i => i.ProductId);

            var products = new List<Product>();

            foreach (var id in itemIds)
            {
                var product = this.dbContext.Products.SingleOrDefault(x => x.Id == id);
                products.Add(product);
            }

            var model = this.mapper.Map<IEnumerable<ProductCartViewModel>>(products).ToList();

            for (var i = 0; i < model.Count(); i++)
            {
                model[i].Quantity = cartItems[i].Quantity;
                model[i].Weight = cartItems[i].Weight;
                model[i].Price = Math.Round(model[i].Price - ((model[i].Price * model[i].Discount) / 100), 2);
            }

            return model;
        }
    }
}