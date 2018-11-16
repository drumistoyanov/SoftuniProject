﻿using System.Collections.Generic;
using GroceryStore.Data.Common.Models;

namespace GroceryStore.Data.Models
{
    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.OrderProducts=new HashSet<OrderProduct>();
        }

        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } 
    }
}
