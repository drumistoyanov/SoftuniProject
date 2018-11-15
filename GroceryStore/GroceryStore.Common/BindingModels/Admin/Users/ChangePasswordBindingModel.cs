using System.ComponentModel.DataAnnotations;
using GroceryStore.Common.Constants;

namespace GroceryStore.Common.BindingModels.Admin.Users
{
    public class ChangePasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = ValidationConstants.NewPassword)]
        public string NewPassword { get; set; }
    }
}
