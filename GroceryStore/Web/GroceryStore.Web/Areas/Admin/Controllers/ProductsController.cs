using System.Linq;
using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Products;
using GroceryStore.Common.Constants.AreaAdmin;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly IAdminProductsService adminProductsService;

        public ProductsController(IAdminProductsService adminProductsService)
        {
            this.adminProductsService = adminProductsService;
        }

        public IActionResult Index(int id)
        {
            var page = id;
            var manufacturersCount = adminProductsService.GetProducts().ToList().Count;
            if (page <= 0 || page > manufacturersCount)
            {
                page = 1;
            }
            ViewData[AdminConstants.PagesCount] = (manufacturersCount / 6) + 1;
            ViewData[AdminConstants.CurrentPage] = page;
            var skip = (page - 1) * 6;

            var model = adminProductsService.GetProducts()
                .Skip(skip)
                .Take(6);

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await adminProductsService.GetDetails(id);

            return View(model);
        }

        public IActionResult Create()
        {
            var model = adminProductsService.GetBindingModel();
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Create(ProductBindingModel model)
        {
            await adminProductsService.SaveProduct(model);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyAdd,
                AdminConstants.Product);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await adminProductsService.GetProduct(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Edit(int id, ProductBindingModel model)
        {
            await adminProductsService.EditProduct(id, model);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyEdit,
                AdminConstants.Product);

            return RedirectToAction(AdminConstants.ActionDetails, new { id });
        }

        public IActionResult Delete(int id)
        {
            ViewData[AdminConstants.ProductId] = id;

            return View();
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await adminProductsService.DeleteProduct(id);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyDelete,
                AdminConstants.Product);

            return RedirectToAction(nameof(Index));
        }
    }
}