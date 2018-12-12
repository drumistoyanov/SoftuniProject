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
            var manufacturers = DbContext.Manufacturers
                .Include(p=>p.Products)
                .ToList();

            var model= Mapper.Map<IEnumerable<ManufacturerIndexViewModel>>(manufacturers);

            return model;
        }

        public async Task<ManufacturerDetailsViewModel> GetDetails(int id)
        {
            var manufacturer = await DbContext.Manufacturers
                .Include(p => p.Products)
                .SingleOrDefaultAsync(x => x.Id == id);
            CheckIfManufacturerExist(manufacturer);

            var model= Mapper.Map<ManufacturerDetailsViewModel>(manufacturer);

            return model;
        }

        public async Task SaveManufacturer(ManufacturerBindingModel model)
        {
            var manufacturer = Mapper.Map<Manufacturer>(model);
            await DbContext.Manufacturers.AddAsync(manufacturer);
            await DbContext.SaveChangesAsync();
        }

        public async Task<ManufacturerBindingModel> GetManufacturer(int id)
        {
            var manufacturer = await DbContext.Manufacturers.FindAsync(id);
            CheckIfManufacturerExist(manufacturer);

            return Mapper.Map<ManufacturerBindingModel>(manufacturer);
        }

        public async Task DeleteManufacturer(int id)
        {
            var manufacturer = await DbContext.Manufacturers.FindAsync(id);
            CheckIfManufacturerExist(manufacturer);

            DbContext.Remove(manufacturer);
            await DbContext.SaveChangesAsync();
        }

        public async Task EditManufacturer(int id,ManufacturerBindingModel model)
        {
            var manufacturer = await DbContext.Manufacturers.FindAsync(id);
            CheckIfManufacturerExist(manufacturer);

            manufacturer.Name = model.Name;
            manufacturer.LogoUrl = model.LogoUrl;

            await DbContext.SaveChangesAsync();
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
