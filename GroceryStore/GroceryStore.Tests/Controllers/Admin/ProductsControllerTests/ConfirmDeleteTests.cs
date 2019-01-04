using GroceryStore.Services.Admin.Interfaces;
using GroceryStore.Web.Areas.Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GroceryStore.Tests.Controllers.Admin.ProductsControllerTests
{
    [TestClass]
    public class ConfirmDeleteTests
    {
        [TestMethod]
        public void ConfirmDelete_WithIdCallServiceGetProduct()
        {
            var serviceCalled = false;
            var mockReposity = new Mock<IAdminProductsService>();
            mockReposity.
                Setup(repo => repo.DeleteProduct(1))
                .Callback(() => serviceCalled = true);

            var controller = new ProductsController(mockReposity.Object);

            var result = controller.ConfirmDelete(1);

            Assert.IsTrue(serviceCalled);
        }
    }
}
