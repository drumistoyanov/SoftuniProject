﻿using System.Collections.Generic;
using System.Linq;
using GroceryStore.Common.ViewModels;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Data.Models;
using GroceryStore.Services.ManufacturersProducts.Interfaces;
using GroceryStore.Services.Products.Interfaces;

namespace GroceryStore.Web.Controllers
{
    public abstract class BaseKindManufacturerProductsController : BaseController
    {
        private readonly string _controllerName;

        protected BaseKindManufacturerProductsController(
            IManufacturerProductsServices manufacturerProductsServices,
            IProductsService productsService, 
            string controllerName)
        {
            this._controllerName = controllerName;
            ManufacturerProductsServices = manufacturerProductsServices;
            ProductsService = productsService;
        }

        protected IProductsService ProductsService { get; private set; }

        protected IManufacturerProductsServices ManufacturerProductsServices { get; private set; }

        protected ManufacturerProductsViewModel Index(int id, int page,string kind)
        {
            var productsCount = ManufacturerProductsServices.GetManufacturerWithProducts(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = ManufacturerProductsServices.GetManufacturerWithProducts(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = ManufacturerProductsViewModel(id, page, maxPage, nameof(Index), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel PriceHighLow(int id, int page, string kind)
        {
            var productsCount = ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceDescending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceDescending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = ManufacturerProductsViewModel(id, page, maxPage, nameof(PriceHighLow), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel PriceLowHigh(int id, int page, string kind)
        {
            var productsCount = ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceAscending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = ManufacturerProductsServices.GetManufacturerWithProductsOrderByPriceAscending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = ManufacturerProductsViewModel(id, page, maxPage, nameof(PriceLowHigh), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel DiscountHighLow(int id, int page, string kind)
        {
            var productsCount = ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountDescending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountDescending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = ManufacturerProductsViewModel(id, page, maxPage, nameof(DiscountHighLow), manufacturerWithProducts);

            return model;
        }

        protected ManufacturerProductsViewModel DiscountLowHigh(int id, int page, string kind)
        {
            var productsCount = ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountAscending(id, kind)
                .Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var manufacturerWithProducts = ManufacturerProductsServices.GetManufacturerWithProductsOrderByDiscountAscending(id, kind);
            manufacturerWithProducts.Products = manufacturerWithProducts.Products
                .Skip(skip)
                .Take(12)
                .ToList();

            var model = ManufacturerProductsViewModel(id, page, maxPage, nameof(DiscountLowHigh), manufacturerWithProducts);

            return model;
        }
        
        private IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var manufacturers = ProductsService.GetAllManufacturers();

            return manufacturers;
        }

        private ManufacturerProductsViewModel ManufacturerProductsViewModel(int id, int currentPage, int maxPage,
            string actionName, ManufacturerDetailsViewModel manufacturerDetailsViewModel)
        {
            var pageViewModel = new PagesViewModel
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = _controllerName,
                RouteId = id
            };

            var manufacturers = GetAllManufacturers();

            return new ManufacturerProductsViewModel
            {
                Manufacturers = manufacturers,
                ManufacturerWithProducts = manufacturerDetailsViewModel,
                Page = pageViewModel
            };
        }
    }
}