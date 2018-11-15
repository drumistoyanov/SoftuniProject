using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GroceryStore.Common.Constants;
using GroceryStore.Data.Models;

namespace GroceryStore.Common.BindingModels.Admin.Products
{
    public class ProductBindingModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [Url]
        [Display(Name = ValidationConstants.PictureUrl)]
        public string PictureUrl { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = ValidationConstants.ErrorMessageForMinAndMaxLength, MinimumLength = 3)]
        public string Type { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        public decimal Weight { get; set; }

        [Range(typeof(decimal), ValidationConstants.MinDecimal, ValidationConstants.MaxDecimal)]
        public decimal Discount { get; set; }

        [Range(typeof(decimal), ValidationConstants.MinDecimal, ValidationConstants.MaxDecimal)]
        public decimal Price { get; set; }

        [Display(Name = ValidationConstants.ManufacturerName)]
        public int ManufacturerId { get; set; }

        public List<Manufacturer> Manufacturers { get; set; }

    }
}
