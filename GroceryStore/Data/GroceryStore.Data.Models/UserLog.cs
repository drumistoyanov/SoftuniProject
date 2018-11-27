using System;

namespace GroceryStore.Data.Models
{
    public class UserLog 
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}
