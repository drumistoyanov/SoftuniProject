using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Images;
using GroceryStore.Common.Constants.AreaAdmin;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Areas.Admin.Controllers
{
    public class ImagesController : AdminController
    {
        private readonly IAdminImagesService adminImagesService;

        public ImagesController(IAdminImagesService adminImagesService)
        {
            this.adminImagesService = adminImagesService;
        }

        public IActionResult Create(int id)
        {
            ViewData[AdminConstants.ProductId] = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Create(int id, ImageBindingModel model)
        {
            await adminImagesService.SaveImage(model, id);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyAdd,
                AdminConstants.Image);

            return RedirectToAction(AdminConstants.ActionDetails, AdminConstants.ActionProducts, 
                new {id });
        }

        public async Task<IActionResult> Edit(int productId, int imageId)
        {
            ViewData[AdminConstants.ProductId] = productId;
            var model = await adminImagesService.GetImage(imageId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Edit(int productId, int imageId, ImageBindingModel model)
        {
            await adminImagesService.EditProduct(imageId, model);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyEdit,
                AdminConstants.Image);

            return RedirectToAction(AdminConstants.ActionDetails, AdminConstants.ActionProducts,
                new { id = productId });
        }

        public IActionResult Delete(int productId, int imageId)
        {
            ViewData[AdminConstants.ImageId] = imageId;
            ViewData[AdminConstants.ProductId] = productId;
            return View();
        }

        public async Task<IActionResult> ConfirmDelete(int productId, int imageId)
        {
            await adminImagesService.DeleteProduct(imageId);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyDelete,
               AdminConstants.Image);

            return RedirectToAction(AdminConstants.ActionDetails, AdminConstants.ActionProducts,
                new { id = productId });
        }
    }
}