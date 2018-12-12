using System.Threading.Tasks;
using GroceryStore.Data.Models;
using GroceryStore.Web.Areas.Identity.Pages.Account.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryStore.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class ResetPasswordModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly UserManager<User> userManager;

        public ResetPasswordModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public ResetPasswordInputModel Input { get; set; }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }

            Input = new ResetPasswordInputModel
            {
                Code = code
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
