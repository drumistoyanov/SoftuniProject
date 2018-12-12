using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GroceryStore.Common.ViewModels.Orders;
using GroceryStore.Data;
using GroceryStore.Services.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Services.Orders
{
    public class OrdersService : BaseEFService, IOrdersService
    {
        public OrdersService(GroceryStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
        
        public  IEnumerable<UserOrdersViewModel> GetUserOrders(string userId)
        {
            var userOrders = DbContext.Orders
                .Include(o => o.OrderProducts)
                .Where(u => u.UserId == userId)
                .ToList();

            return Mapper.Map<IEnumerable<UserOrdersViewModel>>(userOrders);
        }

        public IEnumerable<OrderProductsViewModel> GetOrderProducts(int orderId)
        {
            var orderProducts = DbContext.OrderProducts
                .Where(o=>o.OrderId==orderId)
                .ToList();

            return Mapper.Map<IEnumerable<OrderProductsViewModel>>(orderProducts);
        }
    }
}
