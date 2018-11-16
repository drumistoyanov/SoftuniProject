using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Types.Interfaces;

namespace GroceryStore.Services.Types
{
    public class TypesService : BaseEFService, ITypesService
    {
        public TypesService(GroceryStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<ProductIndexViewModel> GetProducts(string[] brandNames, string kind)
        {
            var products = this.DbContext.Products
                .Where(x => x.Kind.ToString().ToLower() == kind.ToLower()
                && brandNames.Contains(x.Type))
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string[] brandNames, 
            string kind)
        {
            var products = this.DbContext.Products
                .Where(x => x.Kind.ToString().ToLower() == kind.ToLower()
                && brandNames.Contains(x.Type))
                .OrderByDescending(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string[] brandNames,
            string kind)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Kind.ToString().ToLower() == kind.ToLower()
                && brandNames.Contains(x.Type))
                 .OrderBy(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string[] brandNames,
            string kind)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Kind.ToString().ToLower() == kind.ToLower()
                && brandNames.Contains(x.Type))
                 .OrderByDescending(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string[] brandNames, 
            string kind)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Kind.ToString().ToLower() == kind.ToLower()
                && brandNames.Contains(x.Type))
                 .OrderBy(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }
        
        public IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string[] brandNames,
            string kind)
        {
            var orderProducts = this.DbContext.OrderProducts.ToList();
            var productsIdContsDic = new Dictionary<int, int>();
            foreach (var orderProduct in orderProducts)
            {
                if (!productsIdContsDic.ContainsKey(orderProduct.ProductId))
                {
                    productsIdContsDic.Add(orderProduct.ProductId, 1);
                }
                productsIdContsDic[orderProduct.ProductId] += orderProduct.Quantity;
            }

            var ordederedProducts = new List<Product>();
            var orderedProductsIds = new List<int>();

            foreach (var kvp in productsIdContsDic.OrderByDescending(x => x.Value))
            {
                var product = this.DbContext.Products
                    .SingleOrDefault(x => x.Id == kvp.Key && x.Kind.ToString() == kind
                    && brandNames.Contains(x.Type));

                if (product != null)
                {
                    ordederedProducts.Add(product);
                    orderedProductsIds.Add(kvp.Key);
                }
            }

            var otherProducts = this.DbContext.Products
                .Where(x => x.Kind.ToString() == kind && brandNames.Contains(x.Type)
                && !(orderedProductsIds.Contains(x.Id)))
                .ToList();

            var products = ordederedProducts.Concat(otherProducts).ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }
    }
}
