using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GroceryStore.Common.Constants;
using GroceryStore.Data.Models;

namespace GroceryStore.Common.BindingModels.Admin.Manufacturers
{
    public class ManufacturerBindingModel
    {
        [Required]
        [StringLength(30, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 5)]
        [Display(Name = ValidationConstants.Name)]
        public string Name { get; set; }

        public int Id { get; set; }

        public string City { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        [Required]
        [Url]
        [Display(Name = ValidationConstants.LogoUrl)]
        public string LogoUrl { get; set; }
    }
}
