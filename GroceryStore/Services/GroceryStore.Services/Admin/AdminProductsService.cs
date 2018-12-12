using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStore.Common.BindingModels.Admin.Products;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Services.Admin
{
    public class AdminProductsService : BaseEFService, IAdminProductsService
    {
        public AdminProductsService(GroceryStoreDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public IEnumerable<ProductIndexViewModel> GetProducts()
        {
            var products = DbContext.Products.ToList();

            return Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public async Task<ProductDetailsViewModel> GetDetails(int id)
        {
            var product = await DbContext.Products
                .Include(t => t.Manufacturer)
                .Include(i=>i.Images)
                .SingleOrDefaultAsync(x => x.Id == id);
            CheckIfProductExist(product);

            var model = Mapper.Map<ProductDetailsViewModel>(product);

            return model;
        }

        public async Task<ProductBindingModel> GetProduct(int id)
        {
            var product = await DbContext.Products
                .FindAsync(id);
            CheckIfProductExist(product);
            
            var bindingModel = Mapper.Map<ProductBindingModel>(product);

            return bindingModel;
        }

        public ProductBindingModel GetBindingModel()
        {
            var allmanufacturers = DbContext.Manufacturers.ToList();

            var productBindingModel = new ProductBindingModel
            {
                Manufacturers = allmanufacturers
            };

            return productBindingModel;
        }

        public async Task SaveProduct(ProductBindingModel model)
        {
            var manufacturer = await DbContext.Manufacturers.FindAsync(model.ManufacturerId);
            CheckIfManufacturerExist(manufacturer);

            var product = Mapper.Map<Product>(model);
            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await DbContext.Products.FindAsync(id);
            CheckIfProductExist(product);

            DbContext.Remove(product);
            await DbContext.SaveChangesAsync();
        }

        public async Task EditProduct(int id, ProductBindingModel model)
        {
            var product = await DbContext.Products.FindAsync(id);
            CheckIfProductExist(product);
            
            product.Name = model.Name;
            product.Type = model.Type;
            product.Discount = model.Discount;
            product.Kind = model.Kind;
            product.Price = model.Price;
            product.Type = model.Type;
            product.PictureUrl = model.PictureUrl;
                
            await DbContext.SaveChangesAsync();
        }

        public async Task SetWeightToProductById(int id,decimal weight)
        {
            var product = await DbContext.Products.FindAsync(id);
            CheckIfProductExist(product);

            product.Weight = weight;
            DbContext.SaveChanges();
        }

        private void CheckIfProductExist(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void CheckIfManufacturerExist(Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
