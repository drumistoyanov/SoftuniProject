using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Users;
using GroceryStore.Common.ViewModels.Admin.Users;
using Microsoft.AspNetCore.Identity;

namespace GroceryStore.Services.Admin.Interfaces
{
    public interface IAdminUsersService
    {
        Task<IEnumerable<UserIndexViewModel>> GetUsers(ClaimsPrincipal sessionUser);

        Task<UserDetailsViewModel> GetUserDatails(string id);

        Task<IdentityResult> ChangeUserPassword(string id, ChangePasswordBindingModel model);

        Task BanUser(string id);

        Task DeleteUser(string id);
    }
}
