using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using GroceryStore.Data.Common;
using GroceryStore.Data.Common.Models;

namespace GroceryStore.Data.Models
{
    public class Image : BaseModel<int>
    {
        [Required]
        [Url]
        public string Url { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
