using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Orders;
using GroceryStore.Services.Orders.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryStore.Web.Pages.Orders
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IOrdersService ordersService;

        public DetailsModel(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public IEnumerable<OrderProductsViewModel> ViewModel { get; set; }

        public void OnGet(int id)
        {
            this.ViewModel = this.ordersService.GetOrderProducts(id);
        }
    }
}