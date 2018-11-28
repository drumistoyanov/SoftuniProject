﻿using System.Collections.Generic;
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
        private readonly IOrdersService ordersService;
        private readonly UserManager<User> userManager;

        public MyOrdersModel(IOrdersService ordersService,
            UserManager<User> userManager)
        {
            this.ordersService = ordersService;
            this.userManager = userManager;
        }

        public IEnumerable<UserOrdersViewModel> ViewModel { get; set; }
        
        public void OnGet()
        {
            var userId = this.userManager.GetUserId(User);
            var model = this.ordersService.GetUserOrders(userId);

            this.ViewModel = model;
        }
    }
}