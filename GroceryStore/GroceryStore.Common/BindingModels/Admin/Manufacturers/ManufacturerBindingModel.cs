using System.ComponentModel.DataAnnotations;
using GroceryStore.Common.Constants;

namespace GroceryStore.Common.BindingModels.Admin.Manufacturers
{
    public class ManufacturerBindingModel
    {
        [Required]
        [StringLength(30, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 5)]
        [Display(Name = ValidationConstants.Name)]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = ValidationConstants.LogoUrl)]
        public string LogoUrl { get; set; }
    }
}
