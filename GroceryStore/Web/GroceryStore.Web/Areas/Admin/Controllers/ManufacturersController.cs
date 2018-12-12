using System.Linq;
using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Manufacturers;
using GroceryStore.Common.Constants.AreaAdmin;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Areas.Admin.Controllers
{
    public class ManufacturersController : AdminController
    {
        private readonly IAdminManufacturersService adminManufacturersService;

        public ManufacturersController(IAdminManufacturersService adminManufacturersService)
        {
            this.adminManufacturersService = adminManufacturersService;
        }

        public IActionResult Index(int id)
        { 
            var page = id;
            var manufacturersCount = adminManufacturersService.GetManufacturers().ToList().Count;
            if (page <= 0 || page> manufacturersCount)
            {
                page = 1;
            }
            ViewData[AdminConstants.PagesCount] = (manufacturersCount / 4) + 1;
            ViewData[AdminConstants.CurrentPage] = page;
            var skip = (page - 1) * 4;

            var model = adminManufacturersService.GetManufacturers()
                .Skip(skip)
                .Take(4);
            return View(model);
        }
        
        public async Task<IActionResult> Details(int id,int page)
        {
            var model = await adminManufacturersService.GetDetails(id);
            var productsCount = model.Products.ToList().Count;
            if (page <= 0 || page > productsCount)
            {
                page = 1;
            }
            ViewData[AdminConstants.PagesCount] = (productsCount / 6) + 1;
            ViewData[AdminConstants.CurrentPage] = page;
            ViewData[AdminConstants.RouteId] = id;
            var skip = (page - 1) * 6;

            var products = model.Products
                .Skip(skip)
                .Take(6)
                .ToList();

            model.Products = products;

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Create(ManufacturerBindingModel model)
        {
            await adminManufacturersService.SaveManufacturer(model);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyAdd,
                AdminConstants.Manufacturer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await adminManufacturersService.GetManufacturer(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> Edit(int id, ManufacturerBindingModel model)
        {
            await adminManufacturersService.EditManufacturer(id, model);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyEdit,
                AdminConstants.Manufacturer);

            return RedirectToAction(AdminConstants.ActionDetails, new {id });
        }

        public IActionResult Delete(int id)
        {
            ViewData[AdminConstants.ManufacturerId] = id;

            return View();
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await adminManufacturersService.DeleteManufacturer(id);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = string.Format(AdminConstants.SuccessfullyDelete,
                AdminConstants.Manufacturer);

            return RedirectToAction(nameof(Index));
        }
    }
}