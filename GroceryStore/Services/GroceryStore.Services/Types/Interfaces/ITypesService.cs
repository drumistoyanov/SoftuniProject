using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Admin.Products;

namespace GroceryStore.Services.Types.Interfaces
{
    public interface ITypesService
    {
        IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string[] brandNames,string kind);

        IEnumerable<ProductIndexViewModel> GetProducts(string[] brands, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string[] brandNames, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string[] brandNames, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string[] brandNames, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string[] brandNames, string kind);
    }
}
