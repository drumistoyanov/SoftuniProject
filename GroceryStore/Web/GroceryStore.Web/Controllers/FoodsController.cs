    using System.Linq;
    using GroceryStore.Common.Constants;
    using GroceryStore.Services.Products.Interfaces;
    using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public class FoodsController:BaseKindProductsController
    {
        public FoodsController(IProductsService productsService)
            : base(productsService, ControllersConstants.Foods)
        {
        }
        
        public IActionResult Index(int id)
        {
            var model = Index(id, ControllersConstants.Foods);
            
            return View(model);
        }

        [HttpPost]
        public IActionResult SelectType(string[] types)
        {
            if (types.Count() == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), ControllersConstants.Types, 
                new {types, kind = ControllersConstants.Foods });
        }

        public IActionResult PriceHighLow(int id)
        {
            var model = PriceHighLow(id, ControllersConstants.Foods);

            return View(model);
        }

        public IActionResult PriceLowHigh(int id)
        {
            var model = PriceLowHigh(id, ControllersConstants.Foods);

            return View(model);
        }

        public IActionResult DiscountHighLow(int id)
        {
            var model = DiscountHighLow(id, ControllersConstants.Foods);

            return View(model);
        }

        public IActionResult DiscountLowHigh(int id)
        {
            var model = DiscountLowHigh(id, ControllersConstants.Foods);

            return View(model);
        }

        public IActionResult TopSellers(int id)
        {
            var model = TopSellers(id, ControllersConstants.Foods);

            return View(model);
        }
    }
}
