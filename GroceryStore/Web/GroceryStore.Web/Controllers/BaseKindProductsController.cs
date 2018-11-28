

using System.Collections.Generic;
using System.Linq;
using GroceryStore.Common.ViewModels;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data.Models;
using GroceryStore.Services.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public abstract class BaseKindProductsController : BaseController
    {
        private string controllerName;

        protected BaseKindProductsController(
            IProductsService productsService, string controllerName)
        {
            this.controllerName = controllerName;
            this.ProductsService = productsService;
        }
        
        protected IProductsService ProductsService { get; private set; }

        protected ProductsViewModel Index(int id,string kind)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProducts(kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProducts(kind)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(Index), products);

            return model;
        }

        protected ProductsViewModel PriceHighLow(int id, string kind)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByPriceDescending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByPriceDescending(kind)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(PriceHighLow), products);

            return model;
        }

        protected ProductsViewModel PriceLowHigh(int id, string kind)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByPriceAscending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByPriceAscending(kind)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(PriceLowHigh), products);

            return model;
        }

        protected ProductsViewModel DiscountHighLow(int id, string kind)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByDiscountDescending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByDiscountDescending(kind)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(DiscountHighLow), products);

            return model;
        }

        protected ProductsViewModel DiscountLowHigh(int id, string kind)
        {
            var page = id;
            var productsCount = this.ProductsService.GetProductsOrderByDiscountAscending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetProductsOrderByDiscountAscending(kind)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(DiscountLowHigh), products);

            return model;
        }

        protected ProductsViewModel TopSellers(int id, string kind)
        {
            var page = id;
            var productsCount = this.ProductsService.GetTheMostSellableProducts(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.ProductsService.GetTheMostSellableProducts(kind)
                .Skip(skip)
                .Take(12);

            var model = this.CreateProductsModel(page, maxPage, nameof(TopSellers), products);

            return model;
        }

        private IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var manufacturers = this.ProductsService.GetAllManufacturers();

            return manufacturers;
        }

        private IEnumerable<AllTypesViewModel> GetAllTypes()
        {
            var types = this.ProductsService.GetAllTypes();

            return types;
        }

        private ProductsViewModel CreateProductsModel(int currentPage, int maxPage,
           string actionName, IEnumerable<ProductIndexViewModel> products)
        {
            var pageViewModel = new PagesViewModel()
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = controllerName
            };

            var manufacturers = this.GetAllManufacturers();
            var types = this.GetAllTypes();

            return new ProductsViewModel()
            {
                Types=types,
                Manufacturers = manufacturers,
                Products = products,
                Page = pageViewModel
            };
        }
    }
}