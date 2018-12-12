using System.Collections.Generic;
using System.Linq;
using GroceryStore.Common.ViewModels;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data.Models;
using GroceryStore.Services.Products.Interfaces;

namespace GroceryStore.Web.Controllers
{
    public abstract class BaseKindProductsController : BaseController
    {
        private readonly string _controllerName;

        protected BaseKindProductsController(
            IProductsService productsService, string controllerName)
        {
            _controllerName = controllerName;
            ProductsService = productsService;
        }
        
        protected IProductsService ProductsService { get; private set; }

        protected ProductsViewModel Index(int id,string kind)
        {
            var page = id;
            var productsCount = ProductsService.GetProducts(kind).ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = ProductsService.GetProducts(kind)
                .Skip(skip)
                .Take(12);

            var model = CreateProductsModel(page, maxPage, nameof(Index), products);

            return model;
        }

        protected ProductsViewModel PriceHighLow(int id, string kind)
        {
            var page = id;
            var productsCount = ProductsService.GetProductsOrderByPriceDescending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = ProductsService.GetProductsOrderByPriceDescending(kind)
                .Skip(skip)
                .Take(12);

            var model = CreateProductsModel(page, maxPage, nameof(PriceHighLow), products);

            return model;
        }

        protected ProductsViewModel PriceLowHigh(int id, string kind)
        {
            var page = id;
            var productsCount = ProductsService.GetProductsOrderByPriceAscending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = ProductsService.GetProductsOrderByPriceAscending(kind)
                .Skip(skip)
                .Take(12);

            var model = CreateProductsModel(page, maxPage, nameof(PriceLowHigh), products);

            return model;
        }

        protected ProductsViewModel DiscountHighLow(int id, string kind)
        {
            var page = id;
            var productsCount = ProductsService.GetProductsOrderByDiscountDescending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = ProductsService.GetProductsOrderByDiscountDescending(kind)
                .Skip(skip)
                .Take(12);

            var model = CreateProductsModel(page, maxPage, nameof(DiscountHighLow), products);

            return model;
        }

        protected ProductsViewModel DiscountLowHigh(int id, string kind)
        {
            var page = id;
            var productsCount = ProductsService.GetProductsOrderByDiscountAscending(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = ProductsService.GetProductsOrderByDiscountAscending(kind)
                .Skip(skip)
                .Take(12);

            var model = CreateProductsModel(page, maxPage, nameof(DiscountLowHigh), products);

            return model;
        }

        protected ProductsViewModel TopSellers(int id, string kind)
        {
            var page = id;
            var productsCount = ProductsService.GetTheMostSoldProducts(kind)
                .ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            var maxPage = (productsCount / 12) + 1;

            var skip = (page - 1) * 12;

            var products = ProductsService.GetTheMostSoldProducts(kind)
                .Skip(skip)
                .Take(12);

            var model = CreateProductsModel(page, maxPage, nameof(TopSellers), products);

            return model;
        }

        private IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var manufacturers = ProductsService.GetAllManufacturers();

            return manufacturers;
        }

        private IEnumerable<AllTypesViewModel> GetAllTypes()
        {
            var types = ProductsService.GetAllTypes();

            return types;
        }

        private ProductsViewModel CreateProductsModel(int currentPage, int maxPage,
           string actionName, IEnumerable<ProductIndexViewModel> products)
        {
            var pageViewModel = new PagesViewModel
            {
                CurrentPage = currentPage,
                MaxPage = maxPage,
                AreaName = "",
                ActionName = actionName,
                ControllerName = _controllerName
            };

            var manufacturers = GetAllManufacturers();
            var types = GetAllTypes();

            return new ProductsViewModel
            {
                Types=types,
                Manufacturers = manufacturers,
                Products = products,
                Page = pageViewModel
            };
        }
    }
}