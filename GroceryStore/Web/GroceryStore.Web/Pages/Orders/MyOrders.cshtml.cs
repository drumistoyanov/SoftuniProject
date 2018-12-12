using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Orders;
using GroceryStore.Data.Models;
using GroceryStore.Services.Orders.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryStore.Web.Pages.Orders
{
    [Authorize]
    public class MyOrdersModel : PageModel
    {
        private readonly IOrdersService _ordersService;
        private readonly UserManager<User> _userManager;

        public MyOrdersModel(IOrdersService ordersService,
            UserManager<User> userManager)
        {
            this._ordersService = ordersService;
            this._userManager = userManager;
        }

        public IEnumerable<UserOrdersViewModel> ViewModel { get; set; }
        
        public void OnGet()
        {
            var userId = _userManager.GetUserId(User);
            var model = _ordersService.GetUserOrders(userId);

            ViewModel = model;
        }
    }
}