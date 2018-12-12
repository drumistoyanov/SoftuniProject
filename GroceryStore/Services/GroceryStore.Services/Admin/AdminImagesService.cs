using System;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStore.Common.BindingModels.Admin.Images;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Admin.Interfaces;

namespace GroceryStore.Services.Admin
{
    public class AdminImagesService : BaseEFService, IAdminImagesService
    {
        public AdminImagesService(GroceryStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public async Task<ImageBindingModel> GetImage(int id)
        {
            var image= await DbContext.Images.FindAsync(id);
            CheckIfImageExist(image);

            return Mapper.Map<ImageBindingModel>(image);
        }

        public async Task SaveImage(ImageBindingModel model, int productId)
        {
            var image = Mapper.Map<Image>(model);
            image.ProductId = productId;
            await DbContext.Images.AddAsync(image);
            await DbContext.SaveChangesAsync();
        }
        
        public async Task EditProduct(int id, ImageBindingModel model)
        {
            var image = await DbContext.Images.FindAsync(id);
            CheckIfImageExist(image);

            image.Url = model.Url;

            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var image = await DbContext.Images.FindAsync(id);
            CheckIfImageExist(image);

            DbContext.Remove(image);
            await DbContext.SaveChangesAsync();
        }

        private void CheckIfImageExist(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException($"Image with {image.Id} id not found!");
            }
        }
    }
}
