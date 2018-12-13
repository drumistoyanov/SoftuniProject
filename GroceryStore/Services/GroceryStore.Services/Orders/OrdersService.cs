using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GroceryStore.Common.ViewModels.Orders;
using GroceryStore.Data;
using GroceryStore.Data.Models;
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
            var orderProducts = new List<OrderProduct>();

            var orderProductsViewModels = new List<OrderProductsViewModel>();

            orderProducts = DbContext.OrderProducts
                .Where(o=>o.OrderId==orderId)
                .ToList();

            foreach (var orderProduct in orderProducts)
            {
                var model1 = new OrderProductsViewModel()
                {
                    ProductName = orderProduct.ProductName,
                    ProductPicture = orderProduct.ProductPicture,
                    ProductPrice = orderProduct.ProductPrice,
                    ProductWeight = orderProduct.ProductWeight,
                    Quantity = orderProduct.Quantity,
                    Weight = orderProduct.Quantity * orderProduct.ProductWeight
                };
                orderProductsViewModels.Add(model1);
            }

            return orderProductsViewModels;
        }
    }
}
