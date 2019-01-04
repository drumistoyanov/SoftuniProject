using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStore.Tests.Controllers.Admin.ProductsControllerTests
{
    [TestClass]
    public class DeleteTests
    {
        [TestMethod]
        public void Delete_ViewModelShouldNotBeNull()
        {
            var mockRepository = new Mock<IAdminProductsService>();

            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Delete(1);

            Assert.IsNotNull(result as ViewResult);
        }

        [TestMethod]
        public void Delete_ShouldReturnTypeofViewModel()
        {
            var mockRepository = new Mock<IAdminProductsService>();

            var controller = new ProductsController(mockRepository.Object);

            var result = controller.Delete(1);
            
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
