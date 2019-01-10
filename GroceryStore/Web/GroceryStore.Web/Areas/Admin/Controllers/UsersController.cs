using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Users;
using GroceryStore.Common.Constants.AreaAdmin;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
       private readonly IAdminUsersService _adminUsersService;

        public UsersController(IAdminUsersService adminUsersService)
        {
            this._adminUsersService = adminUsersService;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _adminUsersService.GetUsers(User);
            return View(model);
        }
        
        public async Task<IActionResult> Details(string id)
        {
            var model = await _adminUsersService.GetUserDatails(id);
            return View(model);
        }
        
        public IActionResult ChangePassword(string id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationModel]
        public async Task<IActionResult> ChangePassword(string id,ChangePasswordBindingModel model)
        {
            var result = await _adminUsersService.ChangeUserPassword(id, model);
            if (!result.Succeeded)
            {
                return View();
            }

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = AdminConstants.PasswordChangeSuccessfully;
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Ban(string id)
        {
            ViewData[AdminConstants.UserId] = id;
            return View();
        }
        
        public async Task<IActionResult> ConfirmBan(string id)
        {
            await _adminUsersService.BanUser(id);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = AdminConstants.SuccessfullyBan;

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(string id)
        {
            ViewData[AdminConstants.UserId] = id;
            return View();
        }
        
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            await _adminUsersService.DeleteUser(id);

            TempData[AdminConstants.MessageType] = AdminConstants.Success;
            TempData[AdminConstants.Message] = AdminConstants.SuccessfullyDeleteUser;

            return RedirectToAction(nameof(Index));
        }
    }
}