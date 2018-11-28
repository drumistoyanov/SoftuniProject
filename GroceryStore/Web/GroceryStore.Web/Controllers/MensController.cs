using System.Linq;
using GroceryStore.Common.Constants;
using GroceryStore.Services.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public class MensController:BaseKindProductsController
    {
        public MensController(IProductsService productsService)
            : base(productsService, ControllersConstants.Mens)
        {
        }
        
        public IActionResult Index(int id)
        {
            var model = this.Index(id, ControllersConstants.Mens);

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
                new { types = types, kind = ControllersConstants.Mens });
        }

        public IActionResult PriceHighLow(int id)
        {
            var model = this.PriceHighLow(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult PriceLowHigh(int id)
        {
            var model = this.PriceLowHigh(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult DiscountHighLow(int id)
        {
            var model = this.DiscountHighLow(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult DiscountLowHigh(int id)
        {
            var model = this.DiscountLowHigh(id, ControllersConstants.Mens);

            return View(model);
        }

        public IActionResult TopSellers(int id)
        {
            var model = this.TopSellers(id, ControllersConstants.Mens);

            return View(model);
        }
    }
}
