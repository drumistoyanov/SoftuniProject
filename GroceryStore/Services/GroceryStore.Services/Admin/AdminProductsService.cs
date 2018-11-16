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
            var products = this.DbContext.Products.ToList();

            return this.Mapper.Map<IEnumerable<ProductIndexViewModel>>(products);
        }

        public async Task<ProductDetailsViewModel> GetDetails(int id)
        {
            var product = await this.DbContext.Products
                .Include(t => t.Manufacturer)
                .Include(i=>i.Images)
                .SingleOrDefaultAsync(x => x.Id == id);
            this.CheckIfProductExist(product);

            var model = this.Mapper.Map<ProductDetailsViewModel>(product);

            return model;
        }

        public async Task<ProductBindingModel> GetProduct(int id)
        {
            var product = await this.DbContext.Products
                .FindAsync(id);
            this.CheckIfProductExist(product);
            
            var bindingModel = this.Mapper.Map<ProductBindingModel>(product);

            return bindingModel;
        }

        public ProductBindingModel GetBindingModel()
        {
            var allmanufacturers = this.DbContext.Manufacturers.ToList();

            var productBindingModel = new ProductBindingModel
            {
                Manufacturers = allmanufacturers
            };

            return productBindingModel;
        }

        public async Task SaveProduct(ProductBindingModel model)
        {
            var manufacturer = await this.DbContext.Manufacturers.FindAsync(model.ManufacturerId);
            this.CheckIfManufacturerExist(manufacturer);

            var product = this.Mapper.Map<Product>(model);
            await this.DbContext.Products.AddAsync(product);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await this.DbContext.Products.FindAsync(id);
            this.CheckIfProductExist(product);

            this.DbContext.Remove(product);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task EditProduct(int id, ProductBindingModel model)
        {
            var product = await this.DbContext.Products.FindAsync(id);
            this.CheckIfProductExist(product);
            
            product.Name = model.Name;
            product.Type = model.Type;
            product.Discount = model.Discount;
            product.Kind = model.Kind;
            product.Price = model.Price;
            product.Type = model.Type;
            product.PictureUrl = model.PictureUrl;
                
            await this.DbContext.SaveChangesAsync();
        }

        public async Task SetSizeToProductById(int id,decimal weight)
        {
            var product = await this.DbContext.Products.FindAsync(id);
            this.CheckIfProductExist(product);

            product.Weight = weight;
            this.DbContext.SaveChanges();
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
