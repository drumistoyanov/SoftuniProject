using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Products;
using GroceryStore.Common.ViewModels.Admin.Products;

namespace GroceryStore.Services.Admin.Interfaces
{
    public interface IAdminProductsService
    {
        IEnumerable<ProductIndexViewModel> GetProducts();

        ProductBindingModel GetBindingModel();

        Task<ProductDetailsViewModel> GetDetails(int id);

        Task<ProductBindingModel> GetProduct(int id);

        Task SaveProduct(ProductBindingModel model);

        Task EditProduct(int id, ProductBindingModel model);

        Task DeleteProduct(int id);

        Task SetSizeToProductById(int id,decimal weight);
    }
}
