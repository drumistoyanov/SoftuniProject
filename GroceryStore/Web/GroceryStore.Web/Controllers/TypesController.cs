﻿using System.Collections.Generic;
using System.Linq;
using GroceryStore.Common.Constants;
using GroceryStore.Common.ViewModels;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data.Models;
using GroceryStore.Services.Products.Interfaces;
using GroceryStore.Services.Types.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public class TypesController: BaseController
    {
        private readonly IProductsService productsService;
        private readonly ITypesService typesService;

        public TypesController(IProductsService productsService,
            ITypesService typesService)
        {
            this.productsService = productsService;
            this.typesService = typesService;
        }

        public IActionResult Index(string[] types, string kind, int page)
        {
            var productsCount = this.typesService.GetProducts(types,kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.typesService.GetProducts(types, kind)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(types, kind,page, maxPage, nameof(Index), products);

            return View(model);
        }

        public IActionResult TopSellers(string[] types, string kind, int page)
        {
            var productsCount = this.typesService.GetTheMostSellableProducts(types, kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.typesService.GetTheMostSellableProducts(types, kind)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(types, kind, page, maxPage, nameof(TopSellers), products);

            return View(model);
        }

        public IActionResult PriceHighLow(string[] types, string kind, int page)
        {
            var productsCount = this.typesService.GetProductsOrderByPriceDescending(types, kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.typesService.GetProductsOrderByPriceDescending(types, kind)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(types, kind, page, maxPage, nameof(PriceHighLow), products);

            return View(model);
        }

        public IActionResult PriceLowHigh(string[] types, string kind, int page)
        {
            var productsCount = this.typesService.GetProductsOrderByPriceAscending(types, kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.typesService.GetProductsOrderByPriceAscending(types, kind)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(types, kind, page, maxPage, nameof(PriceLowHigh), products);

            return View(model);
        }

        public IActionResult DiscountHighLow(string[] types, string kind, int page)
        {
            var productsCount = this.typesService.GetProductsOrderByDiscountDescending(types, kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.typesService.GetProductsOrderByDiscountDescending(types, kind)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(types, kind, page, maxPage, nameof(DiscountHighLow), products);

            return View(model);
        }

        public IActionResult DiscountLowHigh(string[] types, string kind, int page)
        {
            var productsCount = this.typesService.GetProductsOrderByDiscountAscending(types, kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = this.typesService.GetProductsOrderByDiscountAscending(types, kind)
                .Skip(skip)
                .Take(12);
            var model = this.CreateProductsModel(types, kind, page, maxPage, nameof(DiscountLowHigh), products);

            return View(model);
        }

        private TypesProductsViewModel CreateProductsModel(string[] typesNames,string kind,
            int currentPage, int maxPage,
           string actionName, IEnumerable<ProductIndexViewModel> products)
        {
            var pageViewModel = new PagesViewModel()
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = ControllersConstants.Types
            };

            var manufacturers = this.GetAllManufacturers();
            var types = this.CreateTypesViewModel(typesNames);

            return new TypesProductsViewModel()
            {
                Types = types,
                Manufacturers = manufacturers,
                Products = products,
                Page = pageViewModel,
                Kind=kind
            };
        }

        private IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var manufacturers = this.productsService.GetAllManufacturers();

            return manufacturers;
        }

        private IEnumerable<AllTypesViewModel> CreateTypesViewModel(string[] typesNames)
        {
            var allTypes = this.productsService.GetAllTypes();

            foreach(var type in allTypes)
            {
                if (typesNames.Contains(type.Name))
                {
                    type.IsChecked = true;
                }
            }

            return allTypes;
        }
    }
}
