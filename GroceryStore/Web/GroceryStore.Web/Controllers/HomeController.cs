using System.Diagnostics;
using GroceryStore.Common.Constants;
using GroceryStore.Services.Products.Interfaces;

namespace GroceryStore.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {

        public IActionResult Contact()
        {
            return this.View();
        }
     
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchTerm)
        {
            this.ViewData[ControllersConstants.SearchResult] = searchTerm;
            var model = this.productsService.GetProductsBySearchTerm(searchTerm);

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
