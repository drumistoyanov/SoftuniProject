using System;
using GroceryStore.Data.Common.Models;

namespace GroceryStore.Data.Models
{
    public class OrderLog : BaseModel<int>
    {

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public DateTime TimeOfOrder { get; set; }
    }
}
