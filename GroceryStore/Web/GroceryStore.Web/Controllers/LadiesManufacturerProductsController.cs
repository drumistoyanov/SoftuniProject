using GroceryStore.Common.Constants;
using GroceryStore.Services.ManufacturersProducts.Interfaces;
using GroceryStore.Services.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public class LadiesManufacturerProductsController : BaseKindManufacturerProductsController
    {
        public LadiesManufacturerProductsController(
            IManufacturerProductsServices manufacturerProductsServices,
            IProductsService productsService)
            : base(manufacturerProductsServices, productsService, ControllersConstants.LadiesManufacturerProducts)
        {
        }

        public IActionResult Index(int id, int page)
        {
            var model = this.Index(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult PriceHighLow(int id, int page)
        {
            var model = this.PriceHighLow(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult PriceLowHigh(int id, int page)
        {
            var model = this.PriceLowHigh(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult DiscountHighLow(int id, int page)
        {
            var model = this.DiscountHighLow(id, page, ControllersConstants.Ladies);

            return View(model);
        }

        public IActionResult DiscountLowHigh(int id, int page)
        {
            var model = this.DiscountLowHigh(id, page, ControllersConstants.Ladies);

            return View(model);
        }
    }
}