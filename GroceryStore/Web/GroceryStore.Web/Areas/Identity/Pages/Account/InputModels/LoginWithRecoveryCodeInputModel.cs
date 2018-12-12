using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GroceryStore.Web.Areas.Identity.Pages.Account.InputModels
{
    public class LoginWithRecoveryCodeInputModel
    {
        [BindProperty]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
