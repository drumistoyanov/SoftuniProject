using GroceryStore.Common.ViewModels.Admin.Manufacturers;

namespace GroceryStore.Services.ManufacturersProducts.Interfaces
{
    public interface IManufacturerProductsServices
    {
        //Most Sellable - TODO
        ManufacturerDetailsViewModel GetManufacturerWithProducts(int id,string kind);

        ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByPriceAscending(int id, string kind);

        ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByPriceDescending(int id, string kind);

        ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByDiscountAscending(int id, string kind);

        ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByDiscountDescending(int id, string kind);
    }
}
