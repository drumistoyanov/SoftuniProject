using GroceryStore.Common.Constants;
using GroceryStore.Services.ManufacturersProducts.Interfaces;
using GroceryStore.Services.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public class DrinksManufacturerProductsController : BaseKindManufacturerProductsController
    {
        public DrinksManufacturerProductsController(
            IManufacturerProductsServices manufacturerProductsServices,
            IProductsService productsService)
            : base(manufacturerProductsServices, productsService, ControllersConstants.DrinksManufacturerProducts)
        {
        }

        public IActionResult Index(int id, int page)
        {
            var model = Index(id, page, ControllersConstants.Drinks);

            return View(model);
        }

        public IActionResult PriceHighLow(int id, int page)
        {
            var model = PriceHighLow(id, page, ControllersConstants.Drinks);

            return View(model);
        }

        public IActionResult PriceLowHigh(int id, int page)
        {
            var model = PriceLowHigh(id, page, ControllersConstants.Drinks);

            return View(model);
        }

        public IActionResult DiscountHighLow(int id, int page)
        {
            var model = DiscountHighLow(id, page, ControllersConstants.Drinks);

            return View(model);
        }

        public IActionResult DiscountLowHigh(int id, int page)
        {
            var model = DiscountLowHigh(id, page, ControllersConstants.Drinks);

            return View(model);
        }
    }
}