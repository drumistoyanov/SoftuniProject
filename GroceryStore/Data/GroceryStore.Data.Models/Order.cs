using System;
using System.Collections.Generic;

namespace GroceryStore.Data.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderProducts=new HashSet<OrderProduct>();
        }
        public int Id { get; set; }


        public string UserId { get; set; }

        public User User { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } 
    }
}
