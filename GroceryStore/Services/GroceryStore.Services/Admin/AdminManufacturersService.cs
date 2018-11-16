using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStore.Common.BindingModels.Admin.Manufacturers;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Services.Admin
{
    public class AdminManufacturersService : BaseEFService, IAdminManufacturersService
    {
        public AdminManufacturersService(GroceryStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }
        
        public IEnumerable<ManufacturerIndexViewModel> GetManufacturers()
        {
            var manufacturers = this.DbContext.Manufacturers
                .Include(p=>p.Products)
                .ToList();

            var model= this.Mapper.Map<IEnumerable<ManufacturerIndexViewModel>>(manufacturers);

            return model;
        }

        public async Task<ManufacturerDetailsViewModel> GetDetails(int id)
        {
            var manufacturer = await this.DbContext.Manufacturers
                .Include(p => p.Products)
                .SingleOrDefaultAsync(x => x.Id == id);
            this.CheckIfManufacturerExist(manufacturer);

            var model= this.Mapper.Map<ManufacturerDetailsViewModel>(manufacturer);

            return model;
        }

        public async Task SaveManufacturer(ManufacturerBindingModel model)
        {
            var manufacturer = this.Mapper.Map<Manufacturer>(model);
            await this.DbContext.Manufacturers.AddAsync(manufacturer);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<ManufacturerBindingModel> GetManufacturer(int id)
        {
            var manufacturer = await this.DbContext.Manufacturers.FindAsync(id);
            this.CheckIfManufacturerExist(manufacturer);

            return this.Mapper.Map<ManufacturerBindingModel>(manufacturer);
        }

        public async Task DeleteManufacturer(int id)
        {
            var manufacturer = await this.DbContext.Manufacturers.FindAsync(id);
            this.CheckIfManufacturerExist(manufacturer);

            this.DbContext.Remove(manufacturer);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task EditManufacturer(int id,ManufacturerBindingModel model)
        {
            var manufacturer = await this.DbContext.Manufacturers.FindAsync(id);
            this.CheckIfManufacturerExist(manufacturer);

            manufacturer.Name = model.Name;
            manufacturer.LogoUrl = model.LogoUrl;

            await this.DbContext.SaveChangesAsync();
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
