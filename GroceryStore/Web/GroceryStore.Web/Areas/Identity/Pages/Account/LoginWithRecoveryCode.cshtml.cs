using System;
using System.Threading.Tasks;
using GroceryStore.Data.Models;
using GroceryStore.Web.Areas.Identity.Pages.Account.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GroceryStore.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class LoginWithRecoveryCodeModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> logger;

        public LoginWithRecoveryCodeModel(SignInManager<User> signInManager, ILogger<LoginWithRecoveryCodeModel> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public LoginWithRecoveryCodeInputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException("Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException("Unable to load two-factor authentication user.");
            }

            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

            var result = await signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }

            if (result.IsLockedOut)
            {
                logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }

            logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
            ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
            return Page();
        }
    }
}
