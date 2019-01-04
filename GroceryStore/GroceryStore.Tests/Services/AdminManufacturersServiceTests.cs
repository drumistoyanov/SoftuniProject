using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStore.Common.BindingModels.Admin.Manufacturers;
using GroceryStore.Common.Constants;
using GroceryStore.Common.ViewModels.Admin.Manufacturers;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Admin;
using GroceryStore.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryStore.Tests.Services
{
    [TestClass]
    public class AdminManufacturersServiceTests
    {
        private GroceryStoreDbContext dbContext;
        private IMapper mapper;
        private AdminManufacturersService service;

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = MockDbContext.GetContext();
            this.mapper = MockAutoMapper.GetAutoMapper();
            this.service = new AdminManufacturersService(this.dbContext, this.mapper);

            this.dbContext.Manufacturers.Add(new Manufacturer() { Id = 1, Name = string.Format(TestsConstants.Manufacturer, 1),
                LogoUrl=string.Format(TestsConstants.Logo,1)
            });
            this.dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetManufacturers_WithFewManufacturers_ShouldReturnAll()
        {
            this.dbContext.Manufacturers.Add(new Manufacturer() { Id = 2, Name = string.Format(TestsConstants.Manufacturer, 2) });
            this.dbContext.Manufacturers.Add(new Manufacturer() { Id = 3, Name = string.Format(TestsConstants.Manufacturer, 3) });
            this.dbContext.SaveChanges();

            var manufacturers = this.service.GetManufacturers().ToList();

            Assert.IsNotNull(manufacturers);
            Assert.AreEqual(3, manufacturers.Count);
            CollectionAssert.AreEqual(new[] { string.Format(TestsConstants.Manufacturer, 1),
                string.Format(TestsConstants.Manufacturer, 2) ,
                string.Format(TestsConstants.Manufacturer, 3)  },
                manufacturers.Select(t => t.Name).ToArray());
        }

        [TestMethod]
        public void GetManufacturers_WithFewManufacturers_ShouldReturnTypeofIEnumerableManufacturerIndexViewModel()
        {
            this.dbContext.Manufacturers.Add(new Manufacturer() { Id = 2, Name = string.Format(TestsConstants.Manufacturer, 2) });

            this.dbContext.SaveChanges();

            var manufacturers = this.service.GetManufacturers();

            Assert.IsInstanceOfType(manufacturers, typeof(IEnumerable<ManufacturerIndexViewModel>));
        }

        [TestMethod]
        public void GetManufacturers_WithZeroManufacturers_ShouldReturnZero()
        {
            var manufacturerToRemove = this.dbContext.Manufacturers.SingleOrDefault(x => x.Id == 1);
            this.dbContext.Manufacturers.Remove(manufacturerToRemove);
            this.dbContext.SaveChanges();

            var manufacturers = this.service.GetManufacturers().ToList();

            Assert.IsNotNull(manufacturers);
            Assert.AreEqual(0, manufacturers.Count);
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnManufacturerDetailsViewModel()
        {
            this.dbContext.Products.Add(new Product() { Name = string.Format(TestsConstants.Product,1),
                ManufacturerId = 1 });
            this.dbContext.Products.Add(new Product() { Name = string.Format(TestsConstants.Product, 2),
                ManufacturerId = 1 });
            this.dbContext.SaveChanges();

            var manufacturerFromService = await this.service.GetDetails(1);

            var manufacturerDetailsViewModel = new ManufacturerDetailsViewModel()
            {
                Name = string.Format(TestsConstants.Manufacturer, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };

            Assert.AreEqual(manufacturerDetailsViewModel.LogoUrl, manufacturerFromService.LogoUrl);
            Assert.AreEqual(manufacturerDetailsViewModel.Name, manufacturerFromService.Name);
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnTypeofManufacturerDetailsViewModel()
        {
            var manufacturerFromService = await this.service.GetDetails(1);

            var manufacturerDetailsViewModel = new ManufacturerDetailsViewModel()
            {
                Name = string.Format(TestsConstants.Test, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };

            Assert.IsInstanceOfType(manufacturerFromService, typeof(ManufacturerDetailsViewModel));

        }

        [TestMethod]
        public async Task GetManufacturerAsync_WithId_ShouldReturnThisManufacturer()
        {
            var manufacturerFromService = await this.service.GetManufacturer(1);

            var bindingModelTest = new ManufacturerBindingModel()
            {
                Name = string.Format(TestsConstants.Manufacturer, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };

            Assert.AreEqual(bindingModelTest.LogoUrl, manufacturerFromService.LogoUrl);
            Assert.AreEqual(bindingModelTest.Name, manufacturerFromService.Name);
        }

        [TestMethod]
        public async Task GetManufacturerAsync_WithId_ShouldReturnTypeofManufacturerBindingModel()
        {
            var manufacturerFromService = await this.service.GetManufacturer(1);

            var bindingModelTest = new ManufacturerBindingModel()
            {
                Name = string.Format(TestsConstants.Manufacturer, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1)
            };
            
            Assert.IsInstanceOfType(manufacturerFromService, typeof(ManufacturerBindingModel));
        }

        [TestMethod]
        public async Task GetManufacturerAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
             this.service.GetManufacturer(2));
        }

        [TestMethod]
        public async Task SaveManufacturerAsync_WithProperManufacturer_ShouldAddCorrectly()
        {
            var manufacturerToRemove = this.dbContext.Manufacturers.SingleOrDefault(x => x.Id == 1);
            this.dbContext.Manufacturers.Remove(manufacturerToRemove);
            this.dbContext.SaveChanges();

            var manufacturerModel = new ManufacturerBindingModel
            {
                Name = string.Format(TestsConstants.Manufacturer, 1),
                LogoUrl = string.Format(TestsConstants.Logo, 1),
            };

            await this.service.SaveManufacturer(manufacturerModel);

            Assert.AreEqual(1, this.dbContext.Manufacturers.Count());

            var manufacturer = this.dbContext.Manufacturers.Last();

            Assert.AreEqual(string.Format(TestsConstants.Manufacturer, 1), manufacturer.Name);
            Assert.AreEqual(string.Format(TestsConstants.Logo, 1), manufacturer.LogoUrl);
        }

        [TestMethod]
        public async Task DeleteManufacturerAsync_WithId_ShouldDeleteCorrectly()
        {
            await service.DeleteManufacturer(1);

            Assert.AreEqual(0, this.dbContext.Manufacturers.Count());
        }

        [TestMethod]
        public async Task DeleteManufacturerAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this.service.DeleteManufacturer(2));
        }

        [TestMethod]
        public async Task EditManufacturerAsync_WithId_ShouldEditCorrectly()
        {
            var bindingModel = new ManufacturerBindingModel
            {
                Name = string.Format(TestsConstants.EditManufacturer, 1),
                LogoUrl = string.Format(TestsConstants.EditLogo, 1)
            };

            await service.EditManufacturer(1, bindingModel);

            var manufacturer = this.dbContext.Manufacturers.SingleOrDefault(x => x.Id == 1);

            Assert.AreEqual(1, manufacturer.Id);
            Assert.AreEqual(string.Format(TestsConstants.EditManufacturer, 1), manufacturer.Name);
            Assert.AreEqual(string.Format(TestsConstants.EditLogo, 1), manufacturer.LogoUrl);
        }

        [TestMethod]
        public async Task EditManufacturerAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            var bindingModel = new ManufacturerBindingModel
            {
                Name = string.Format(TestsConstants.EditManufacturer, 1),
                LogoUrl = string.Format(TestsConstants.EditLogo, 1)
            };

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this.service.EditManufacturer(2, bindingModel));
        }
    }
}
