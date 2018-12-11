using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Admin.Products;

namespace GroceryStore.Services.Types.Interfaces
{
    public interface ITypesService
    {
        IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string[] brandNames,string kind);

        IEnumerable<ProductIndexViewModel> GetProducts(string[] types, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string[] typeNames, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string[] typeNames, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string[] typeNames, string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string[] typeNames, string kind);
    }
}
