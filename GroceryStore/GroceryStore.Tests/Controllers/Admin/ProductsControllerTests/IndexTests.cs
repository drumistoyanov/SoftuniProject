using System.Collections.Generic;
using GroceryStore.Common.ViewModels.Admin.Products;
using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStore.Tests.Controllers.Admin.ProductsControllerTests
{
    [TestClass]
    public class IndexTests
    {
        [TestMethod]
        public void Index_ViewModelShouldNotBeNull()
        {
            var productModel = new ProductIndexViewModel() { Id = 1, Name = "FirstName" };
            var mockRepository = new Mock<IAdminProductsService>();
            mockRepository
                .Setup(service=>service.GetProducts())
                .Returns(new[]
                {
                    productModel
                });
            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Index(1);

            Assert.IsNotNull((result as ViewResult).Model);
        }

        [TestMethod]
        public void Index_ShouldReturnTypeofViewModel()
        {
            var productModel = new ProductIndexViewModel() { Id = 1, Name = "FirstName" };
            var mockRepository = new Mock<IAdminProductsService>();
            mockRepository
                .Setup(service => service.GetProducts())
                .Returns(new[]
                {
                    productModel
                });
            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Index(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Index_ShouldReturnTypeofIEnuerableProductIndexViewModel()
        {
            var productModel = new ProductIndexViewModel() { Id = 1, Name = "FirstName" };
            var mockRepository = new Mock<IAdminProductsService>();
            mockRepository
                .Setup(service => service.GetProducts())
                .Returns(new[]
                {
                    productModel
                });
            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Index(1);
            var resultView = result as ViewResult;
            Assert.IsInstanceOfType(resultView.Model, typeof(IEnumerable<ProductIndexViewModel>));
        }

        [TestMethod]
        public void Index_ShouldSuccessfullCallMethodGetProducts()
        {
            var methodCalled = false;
            var productModel = new ProductIndexViewModel() { Id = 1, Name = "FirstName" };
            var mockRepository = new Mock<IAdminProductsService>();
            mockRepository
                .Setup(service => service.GetProducts())
                .Returns(new[]
                {
                    productModel
                })
                .Callback(()=>methodCalled=true);
            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Index(1);

            Assert.IsTrue(methodCalled);
        }
    }
}
