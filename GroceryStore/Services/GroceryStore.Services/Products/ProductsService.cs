using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GroceryStore.Common.ViewModels;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Products.Interfaces;

namespace GroceryStore.Services.Products
{
    public class ProductsService : BaseEFService, IProductsService
    {
        public ProductsService(GroceryStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<AllTypesViewModel> GetAllTypes()
        {
            var brands = this.DbContext.Products
                .Select(x => x.Type)
                .ToHashSet();

            var allTypesViewModel = new List<AllTypesViewModel>();
            var id = 0;
            foreach(var brand in brands)
            {
                id++;
                allTypesViewModel.Add(new AllTypesViewModel
                {
                    Id = id,
                    Name = brand
                });
            }

            return allTypesViewModel;
        }

        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var manufacturers = this.DbContext.Manufacturers.ToList();

            return manufacturers;
        }

        public IEnumerable<ProductIndexViewModel> GetProducts(string kind)
        {
            var products = this.DbContext.Products
                .Where(x=>x.Kind.ToString().ToLower()== kind.ToLower())
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string kind)
        {
            var products = this.DbContext.Products
                .Where(x => x.Kind.ToString().ToLower() == kind.ToLower())
                .OrderByDescending(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string kind)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Kind.ToString().ToLower() == kind.ToLower())
                 .OrderBy(x => Math.Round(x.Price - ((x.Price * x.Discount) / 100)))
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string kind)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Kind.ToString().ToLower() == kind.ToLower())
                 .OrderByDescending(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string kind)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Kind.ToString().ToLower() == kind.ToLower())
                 .OrderBy(x => x.Discount)
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetProductsBySearchTerm(string searchTerm)
        {
            var products = this.DbContext.Products
                 .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()))
                 .ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string kind)
        {
            var orderProducts = this.DbContext.OrderProducts.ToList();
            var productsIdContsDic = new Dictionary<int, int>();
            foreach(var orderProduct in orderProducts)
            {
                if (!productsIdContsDic.ContainsKey(orderProduct.ProductId))
                {
                    productsIdContsDic.Add(orderProduct.ProductId, 1);
                }
                productsIdContsDic[orderProduct.ProductId] += orderProduct.Quantity;
            }

            var ordederedProducts = new List<Product>();
            var orderedProductsIds = new List<int>();

            foreach(var kvp in productsIdContsDic.OrderByDescending(x => x.Value))
            {
                var product = this.DbContext.Products
                    .SingleOrDefault(x => x.Id == kvp.Key && x.Kind.ToString() == kind);

                if (product != null)
                {
                    ordederedProducts.Add(product);
                    orderedProductsIds.Add(kvp.Key);
                }
            }

            var otherProducts = this.DbContext.Products
                .Where(x => x.Kind.ToString() == kind && !(orderedProductsIds.Contains(x.Id)))
                .ToList();

            var products = ordederedProducts.Concat(otherProducts).ToList();
            
            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }
    }
}
