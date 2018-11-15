using System;
using System.Collections.Generic;
using GroceryStore.Data.Models;

namespace GroceryStore.Web.ViewModels.Admin.Users
{
    public class UserDetailsViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public List<Order> Orders { get; set; }
    }
}
