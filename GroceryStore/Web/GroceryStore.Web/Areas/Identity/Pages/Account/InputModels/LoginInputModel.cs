﻿using System.ComponentModel.DataAnnotations;

namespace GroceryStore.Web.Areas.Identity.Pages.Account.InputModels
{
    public class LoginInputModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
