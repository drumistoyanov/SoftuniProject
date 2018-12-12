using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Web.Areas.Identity.Pages.Account.Manage.InputModels
{
    public class DeletePersonalDataInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
