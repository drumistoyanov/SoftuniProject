using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data;
using GroceryStore.Services.ManufacturersProducts.Interfaces;

namespace GroceryStore.Services.ManufacturersProducts
{
    public class ManufacturerProductsServices : BaseEFService, IManufacturerProductsServices
    {
        public ManufacturerProductsServices(GroceryStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public ManufacturerDetailsViewModel GetManufacturerWithProducts(int id, string kind)
        {
            var manufacturer = this.DbContext.Manufacturers
              .Select(x => new
              {
                  Id = x.Id,
                  LogoId = x.LogoUrl,
                  Name = x.Name,
                  Products = x.Products.Select(p => new
                  {
                      Id = p.Id,
                      Name = p.Name,
                      Price = p.Price,
                      Discount = p.Discount,
                      PictureUrl = p.PictureUrl,
                      Kind = p.Kind
                  })
                  .Where(p => p.Kind.ToString().ToLower() == kind.ToLower())
              })
              .SingleOrDefault(x => x.Id == id);

            this.CheckIfManufacturerExist(id);

            var model = this.CreateModel(manufacturer);

            return model;
        }
        
        public ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByPriceAscending(int id, string kind)
        {
             var manufacturer = this.DbContext.Manufacturers
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Kind = p.Kind
                   })
                   .Where(p => p.Kind.ToString().ToLower() == kind.ToLower())
                   .OrderBy(pr => Math.Round(pr.Price - ((pr.Price * pr.Discount) / 100)))
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfManufacturerExist(id);

            var model = this.CreateModel(manufacturer);

            return model;
        }

        public ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByPriceDescending(int id, string kind)
        {
            var manufacturer = this.DbContext.Manufacturers
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Kind = p.Kind
                   })
                   .Where(p => p.Kind.ToString().ToLower() == kind.ToLower())
                   .OrderByDescending(pr => Math.Round(pr.Price - ((pr.Price * pr.Discount) / 100)))
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfManufacturerExist(id);
            
            var model = this.CreateModel(manufacturer);

            return model;
        }

        public ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByDiscountAscending(int id, string kind)
        {
            var manufacturer = this.DbContext.Manufacturers
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Kind = p.Kind
                   })
                   .Where(p => p.Kind.ToString().ToLower() == kind.ToLower())
                   .OrderBy(d=>d.Discount)
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfManufacturerExist(id);

            var model = this.CreateModel(manufacturer);

            return model;
        }

        public ManufacturerDetailsViewModel GetManufacturerWithProductsOrderByDiscountDescending(int id, string kind)
        {
            var manufacturer = this.DbContext.Manufacturers
               .Select(x => new
               {
                   Id = x.Id,
                   LogoId = x.LogoUrl,
                   Name = x.Name,
                   Products = x.Products.Select(p => new
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Price = p.Price,
                       Discount = p.Discount,
                       PictureUrl = p.PictureUrl,
                       Kind = p.Kind
                   })
                   .Where(p => p.Kind.ToString().ToLower() == kind.ToLower())
                   .OrderByDescending(d => d.Discount)
               })
               .SingleOrDefault(x => x.Id == id);

            this.CheckIfManufacturerExist(id);

            var model = this.CreateModel(manufacturer);

            return model;
        }

        private void CheckIfManufacturerExist(int id)
        {
            var manufacturer = this.DbContext.Manufacturers.SingleOrDefault(x => x.Id == id);
            if (manufacturer == null)
            {
                throw new ArgumentNullException();
            }
        }

        private ManufacturerDetailsViewModel CreateModel(dynamic manufacturer)
        {
            var model = new ManufacturerDetailsViewModel();
            model.LogoUrl = manufacturer.LogoId;
            model.Name = manufacturer.Name;
            var products = new List<ProductIndexViewModel>();
            foreach (var product in manufacturer.Products)
            {
                products.Add(new ProductIndexViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Discount = product.Discount,
                    PictureUrl = product.PictureUrl
                });
            }
            model.Products = products;

            return model;
        }
    }
}
