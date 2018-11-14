using System;
using GroceryStore.Data.Common.Models;

namespace GroceryStore.Data.Models
{
    public class UserLog : BaseModel<int>
    {
        public int UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}
