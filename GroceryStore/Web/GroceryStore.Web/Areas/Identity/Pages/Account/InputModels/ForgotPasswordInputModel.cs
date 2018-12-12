using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Web.Areas.Identity.Pages.Account.InputModels
{
    public class ForgotPasswordInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
