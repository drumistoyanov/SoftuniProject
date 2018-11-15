using System.ComponentModel.DataAnnotations;
using GroceryStore.Common.Constants;

namespace GroceryStore.Common.BindingModels.Admin.Images
{
    public class ImageBindingModel
    {
        [Required]
        [Url]
        [Display(Name = ValidationConstants.ImageUrl)]
        public string Url { get; set; }
    }
}
