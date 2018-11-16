using GroceryStore.Common.Constants.AreaAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Areas.Admin.Controllers
{
    [Area(AdminConstants.AreaName)]
    [Authorize(Roles = AdminConstants.AdminRoleName)]
    public abstract class AdminController:Controller
    {
    }
}
