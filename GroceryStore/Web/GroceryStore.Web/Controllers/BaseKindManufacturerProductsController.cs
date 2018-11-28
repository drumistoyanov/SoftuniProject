using System.Collections.Generic;
using System.Linq;
using GroceryStore.Common.ViewModels;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Data.Models;
using GroceryStore.Services.ManufacturersProducts.Interfaces;
using GroceryStore.Services.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public abstract class BaseKindManufacturerProductsController : BaseController
    {
        private string controllerName;

        protected BaseKindManufacturerProductsController(
            IManufacturerProductsServices manufacturerProductsServices,
            IProductsService productsService, 
            string controllerName)
        {
            this.controllerName = controllerName;
            this.ManufacturerProductsServices = manufacturerProductsServices;
            this.ProductsService = productsService;
        }

        protected IProductsService ProductsService { get; private set; }

        protected IManufacturerProductsServices ManufacturerProductsServices { get; private set; }

        protected ManufacturerProductsViewModel Index(int id, int page,string kind)
        {
            var manufacturers = this.GetAllManufacturers();
            var productsCount = this.ManufacturerProductsServices.GetManufacturerWithProducts(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = this.ManufacturerProductsServices.GetManufacturerWithProducts(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.ManufacturerProductsViewModel(id, page, maxPage, nameof(Index), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel PriceHighLow(int id, int page, string kind)
        {
            var manufacturers = this.GetAllManufacturers();
            var productsCount = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceDescending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceDescending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.ManufacturerProductsViewModel(id, page, maxPage, nameof(PriceHighLow), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel PriceLowHigh(int id, int page, string kind)
        {
            var manufacturers = this.GetAllManufacturers();
            var productsCount = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceAscending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceAscending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.ManufacturerProductsViewModel(id, page, maxPage, nameof(PriceLowHigh), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel DiscountHighLow(int id, int page, string kind)
        {
            var manufacturers = this.GetAllManufacturers();
            var productsCount = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountDescending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountDescending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.ManufacturerProductsViewModel(id, page, maxPage, nameof(DiscountHighLow), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel DiscountLowHigh(int id, int page, string kind)
        {
            var manufacturers = this.GetAllManufacturers();
            var productsCount = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountAscending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = this.ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountAscending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = this.ManufacturerProductsViewModel(id, page, maxPage, nameof(DiscountLowHigh), manufacturerWithProducts);

            return model;
        }
        
        private IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var manufacturers = this.ProductsService.GetAllManufacturers();

            return manufacturers;
        }

        private ManufacturerProductsViewModel ManufacturerProductsViewModel(int id, int currentPage, int maxPage,
            string actionName, ManufacturerDetailsViewModel manufacturerDetailsViewModel)
        {
            var pageViewModel = new PagesViewModel()
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = controllerName,
                RouteId = id,
            };

            var manufacturers = this.GetAllManufacturers();

            return new ManufacturerProductsViewModel()
            {
                Manufacturers = manufacturers,
                ManufacturerWithProducts = manufacturerDetailsViewModel,
                Page = pageViewModel
            };
        }
    }
}