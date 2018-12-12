using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Web.Areas.Identity.Pages.Account.Manage.InputModels
{
    public class IndexInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
