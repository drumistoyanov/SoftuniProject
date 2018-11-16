using System.Collections.Generic;
using GroceryStore.Common.ViewModels;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data.Models;

namespace GroceryStore.Services.Products.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<ProductIndexViewModel> GetTheMostSellableProducts(string kind);

        IEnumerable<ProductIndexViewModel> GetProductsBySearchTerm(string searchTerm);

        IEnumerable<Manufacturer> GetAllManufacturers();

        IEnumerable<AllTypesViewModel> GetAllTypes();

        IEnumerable<ProductIndexViewModel> GetProducts(string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceDescending(string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByPriceAscending(string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountDescending(string kind);

        IEnumerable<ProductIndexViewModel> GetProductsOrderByDiscountAscending(string kind);
    }
}
