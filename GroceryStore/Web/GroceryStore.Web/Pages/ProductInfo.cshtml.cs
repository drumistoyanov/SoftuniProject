using System.Threading.Tasks;
using GroceryStore.Common.Constants;
using GroceryStore.Common.Constants.AreaAdmin;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryStore.Web.Pages
{
    public class ProductInfoModel : PageModel
    {
        private readonly IAdminProductsService _productsService;
        private readonly ProductCountToBuy _productCountToBuy;

        public ProductInfoModel(IAdminProductsService productsService,ProductCountToBuy productCountToBuy)
        {
            _productsService = productsService;
            _productCountToBuy = productCountToBuy;
        }

        public int Quantity { get; set; } = 1;  
        
        public ProductDetailsViewModel ProductModel { get; set; }

        public async Task OnGet(int id)
        {
            _productCountToBuy.Number = 1;
            ProductModel = await _productsService.GetDetails(id);
        }

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods.
        [ValidateAntiForgeryToken]
#pragma warning restore MVC1001 // Filters cannot be applied to page handler methods.
        public async  Task <IActionResult> OnPost(int id)
        {

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = PagesConstants.AddProductToCart;
            Quantity = _productCountToBuy.Number;
            await Task.Yield();

            return  RedirectToAction(PagesConstants.AddToCart, PagesConstants.ShoppingCart, new {id, quantity = Quantity});
        }

        public async Task OnGetIncreaseCount(int id)
        {
            ProductModel = await _productsService.GetDetails(id);

            _productCountToBuy.Increase();
            Quantity = _productCountToBuy.Number;
        }

        public async Task OnGetDecreaseCount(int id)
        {
            ProductModel = await _productsService.GetDetails(id);

            _productCountToBuy.Decrease();
            Quantity = _productCountToBuy.Number;
        }
    }
}