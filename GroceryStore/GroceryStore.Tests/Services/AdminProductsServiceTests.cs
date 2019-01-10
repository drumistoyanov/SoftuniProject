using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStore.Common.BindingModels.Admin.Products;
using GroceryStore.Common.Constants;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Data;
using GroceryStore.Data.Models;
using GroceryStore.Services.Admin;
using GroceryStore.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryStore.Tests.Services
{
    [TestClass]
    public class AdminProductsServiceTests
    {
        private GroceryStoreDbContext _dbContext;
        private IMapper _mapper;
        private AdminProductsService _service;

        [TestInitialize]
        public void InitializeTests()
        {
            this._dbContext = MockDbContext.GetContext();
            this._mapper = MockAutoMapper.GetAutoMapper();
            this._service = new AdminProductsService(this._dbContext, this._mapper);
            
            this._dbContext.Manufacturers.Add(new Manufacturer() { Id = 1, Name = string.Format(TestsConstants.Manufacturer, 1) });
            this._dbContext.SaveChanges();

            this._dbContext.Products.Add(new Product()
            {
                Id = 1,
                Name = string.Format(TestsConstants.Product, 1),
                Type = string.Format(TestsConstants.Type, 1),
                ManufacturerId = 1
            });
            this._dbContext.SaveChanges();
        }

        [TestMethod]
        public void GetProducts_WithFewProducts_ShouldReturnAll()
        {
            this._dbContext.Products.Add(new Product() { Id = 2, Name = string.Format(TestsConstants.Product, 2),
                ManufacturerId = 1 });
            this._dbContext.Products.Add(new Product() { Id = 3, Name = string.Format(TestsConstants.Product, 3),
                ManufacturerId = 1 });

            this._dbContext.SaveChanges();

            var products = this._service.GetProducts().ToList();

            Assert.IsNotNull(products);
            Assert.AreEqual(3, products.Count);
            CollectionAssert.AreEqual(new[] { string.Format(TestsConstants.Product, 1),
                string.Format(TestsConstants.Product, 2),
                string.Format(TestsConstants.Product, 3)},
                products.Select(t => t.Name).ToArray());
        }

        [TestMethod]
        public void GetProducts_WithFewProducts_ShouldReturnTypeofIEnumerableProductIndexViewModel()
        {
            this._dbContext.Products.Add(new Product() { Id = 2, Name = string.Format(TestsConstants.Product, 2),
                ManufacturerId = 1 });

            this._dbContext.SaveChanges();

            var products= this._service.GetProducts();

            Assert.IsInstanceOfType(products, typeof(IEnumerable<ProductIndexViewModel>));
        }

        [TestMethod]
        public void GetProducts_WithZeroProducts_ShouldReturnZero()
        {
            var product = this._dbContext.Products.SingleOrDefault(x => x.Id == 1);
            this._dbContext.Products.Remove(product);
            this._dbContext.SaveChanges();

            var products = this._service.GetProducts().ToList();

            Assert.IsNotNull(products);
            Assert.AreEqual(0, products.Count);
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnProductDetailsViewModel()
        {
            this._dbContext.Images.Add(new Image() { Id = 1, ProductId=1 });
            this._dbContext.Images.Add(new Image() { Id = 2, ProductId = 1 });
            this._dbContext.SaveChanges();

            var productFromService = await this._service.GetDetails(1);

            var manufacturer = this._dbContext.Manufacturers.SingleOrDefault(x => x.Id == 1);
            var productDetailsViewModel = new ProductDetailsViewModel()
            {
                Name = string.Format(TestsConstants.Product, 1),
                Manufacturer= manufacturer,
                Images = this._dbContext.Images.Where(x => x.ProductId == 1).ToList()
            };

            Assert.AreEqual(productDetailsViewModel.Name, productFromService.Name);
            Assert.AreEqual(productDetailsViewModel.Manufacturer.Name, productFromService.Manufacturer.Name);
            CollectionAssert.AreEqual(productDetailsViewModel.Images.ToList(),
               productFromService.Images.ToList());
        }

        [TestMethod]
        public async Task GetDetailsAsync_WithId_ShouldReturnTypeofProductDetailsViewModel()
        {
            var productFromService = await this._service.GetDetails(1);

            var productDetailsViewModel = new ProductDetailsViewModel()
            {
                Name = string.Format(TestsConstants.Product, 1)
            };

            Assert.IsInstanceOfType(productFromService, typeof(ProductDetailsViewModel));

        }

        [TestMethod]
        public async Task GetProductAsync_WithId_ShouldReturnThisProduct()
        {
            var productFromService = await this._service.GetProduct(1);

            var bindingModelTest = new ProductBindingModel()
            {
                Name = string.Format(TestsConstants.Product, 1),
                Type= string.Format(TestsConstants.Type, 1)
            };

            Assert.AreEqual(bindingModelTest.Name, productFromService.Name);
            Assert.AreEqual(bindingModelTest.Type, productFromService.Type);
        }

        [TestMethod]
        public async Task GetProductAsync_WithId_ShouldReturnTypeofProductBindingModel()
        {
            var productFromService = await this._service.GetProduct(1);

            var bindingModelTest = new ProductBindingModel()
            {
                Name = string.Format(TestsConstants.Test, 1),
                Type = string.Format(TestsConstants.Type, 1)
            };

            Assert.IsInstanceOfType(productFromService, typeof(ProductBindingModel));
        }

        [TestMethod]
        public async Task GetProductAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
             this._service.GetProduct(2));
        }

        [TestMethod]
        public async Task SaveProductAsync_WithProperProduct_ShouldAddCorrectly()
        {
            var productToRemove = this._dbContext.Products.SingleOrDefault(x => x.Id == 1);
            this._dbContext.Products.Remove(productToRemove);
            this._dbContext.SaveChanges();

            var productModel = new ProductBindingModel
            {
               Name = string.Format(TestsConstants.Name, 1),
                Type = string.Format(TestsConstants.Type, 1),
                ManufacturerId= 1,
            };

            await this._service.SaveProduct(productModel);
            
            var product = this._dbContext.Products.First();

            Assert.AreEqual(string.Format(TestsConstants.Name, 1), product.Name);
            Assert.AreEqual(string.Format(TestsConstants.Type, 1), product.Type);
        }

        [TestMethod]
        public async Task DeleteProductAsync_WithId_ShouldDeleteCorrectly()
        {
            await _service.DeleteProduct(1);

            Assert.AreEqual(0, this._dbContext.Products.Count());
        }

        [TestMethod]
        public async Task DeleteProductAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this._service.DeleteProduct(2));
        }

        [TestMethod]
        public async Task EditProductAsync_WithId_ShouldEditCorrectly()
        {
            var bindingModel = new ProductBindingModel
            {
                Name = string.Format(TestsConstants.EditManufacturer, 1),
                Type= string.Format(TestsConstants.EditType, 1)
            };

            await _service.EditProduct(1, bindingModel);

            var product = this._dbContext.Products.SingleOrDefault(x => x.Id == 1);

            Assert.AreEqual(1, product.Id);
            Assert.AreEqual(string.Format(TestsConstants.EditManufacturer, 1), product.Name);
            Assert.AreEqual(string.Format(TestsConstants.EditType, 1), product.Type);
        }

        [TestMethod]
        public async Task EditProductAsync_WithNoValidId_ShouldThrowArgumentNullException()
        {
            var bindingModel = new ProductBindingModel
            {
                Name = string.Format(TestsConstants.EditManufacturer, 1),
                Type = string.Format(TestsConstants.EditManufacturer, 1)
            };

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
            this._service.EditProduct(2, bindingModel));
        }
    }
}
