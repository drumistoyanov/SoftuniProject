using System;

namespace GroceryStore.Data.Models
{
    public class OrderLog 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public DateTime TimeOfOrder { get; set; }
    }
}
