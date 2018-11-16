using System.Threading.Tasks;
using GroceryStore.Common.BindingModels.Admin.Images;

namespace GroceryStore.Services.Admin.Interfaces
{
    public interface IAdminImagesService
    {
        Task<ImageBindingModel> GetImage(int id);

        Task SaveImage(ImageBindingModel model,int productId);

        Task EditProduct(int id, ImageBindingModel model);

        Task DeleteProduct(int id);
    }
}
