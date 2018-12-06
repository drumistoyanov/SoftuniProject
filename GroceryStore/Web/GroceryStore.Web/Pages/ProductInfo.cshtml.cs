using System.Collections.Generic;
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
            this._productsService = productsService;
            this._productCountToBuy = productCountToBuy;
        }

        public int Quantity { get; set; } = 1;  
        
        public ProductDetailsViewModel ProductModel { get; set; }

        public async Task OnGet(int id)
        {
            this._productCountToBuy.Number = 1;
            this.ProductModel = await this._productsService.GetDetails(id);
        }
         
        [ValidateAntiForgeryToken]
        public async  Task <IActionResult> OnPost(int id)
        {

            this.TempData[AdminConstants.MessageType] = AdminConstants.Success;
            this.TempData[AdminConstants.Message] = PagesConstants.AddProductToCart;
            this.Quantity = _productCountToBuy.Number;

            return RedirectToAction(PagesConstants.AddToCart, PagesConstants.ShoppingCart, new {id, quantity = this.Quantity});
        }

        public async Task OnGetIncreaseCount(int id)
        {
            this.ProductModel = await this._productsService.GetDetails(id);

            this._productCountToBuy.Increase();
            this.Quantity = this._productCountToBuy.Number;
        }

        public async Task OnGetDecreaseCount(int id)
        {
            this.ProductModel = await this._productsService.GetDetails(id);

            this._productCountToBuy.Decrease();
            this.Quantity = this._productCountToBuy.Number;
        }
    }
}