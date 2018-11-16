using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Orders;

namespace GroceryStore.Services.Orders.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<UserOrdersViewModel> GetUserOrders(string userId);

        IEnumerable<OrderProductsViewModel> GetOrderProducts(int orderId);
    }
}
