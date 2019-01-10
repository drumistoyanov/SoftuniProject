using System.Diagnostics;
using GroceryStore.Common.Constants;
using GroceryStore.Services.Products.Interfaces;
using GroceryStore.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Controllers
{
    public class HomeController : BaseController
    {

        public IActionResult Contact()
        {
            return View();
        }
     
        private readonly IProductsService _productsService;

        public HomeController(IProductsService productsService)
        {
            this._productsService = productsService;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Search(string searchTerm)
        {
            ViewData[ControllersConstants.SearchResult] = searchTerm;
            var model = _productsService.GetProductsBySearchTerm(searchTerm);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
